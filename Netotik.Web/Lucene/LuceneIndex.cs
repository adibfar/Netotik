using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Search.Similar;
using Lucene.Net.Store;
using SpellChecker.Net.Search.Spell;
using Directory = System.IO.Directory;
using ParseException = Lucene.Net.QueryParsers.ParseException;
using Version = Lucene.Net.Util.Version;
using Netotik.ViewModels;
using Netotik.Common;

namespace Netotik.Web.Lucene
{
    public class LuceneIndex
    {
        private const Version _version = Version.LUCENE_30;

        private static readonly string _luceneDir = HttpRuntime.AppDomainAppPath + @"App_Data\Lucene_Index";


        private static FSDirectory _directory
        {
            get
            {
                //if (_directoryTemp == null)
                var directoryTemp = FSDirectory.Open(new DirectoryInfo(_luceneDir));
                //if (IndexWriter.IsLocked(_directoryTemp))
                //    IndexWriter.Unlock(_directoryTemp);
                //string lockFilePath = Path.Combine(_luceneDir, "write.lock");
                //if (File.Exists(lockFilePath))
                //    File.Delete(lockFilePath);
                return directoryTemp;
            }
        }

        private static void _addToLuceneIndex(ProductSearchModel modelData, IndexWriter writer)
        {
            // remove older index entry

            var searchQuery = new TermQuery(new Term(StronglyTyped.PropertyName<ProductSearchModel>(x => x.Id), modelData.Id.ToString(CultureInfo.InvariantCulture)));


            writer.DeleteDocuments(searchQuery);

            // add new index entry
            var document = new Document();

            // add lucene fields mapped to db fields

            document.Add(new Field(StronglyTyped.PropertyName<ProductSearchModel>(x => x.Id),
           modelData.Id.ToString(CultureInfo.InvariantCulture), Field.Store.YES, Field.Index.NOT_ANALYZED));

            if (modelData.Name != null)
            {
                document.Add(new Field(StronglyTyped.PropertyName<ProductSearchModel>(x => x.Name), modelData.Name, Field.Store.YES, Field.Index.ANALYZED,
                    Field.TermVector.WITH_POSITIONS_OFFSETS)
                {
                    Boost = 3
                });
            }


            if (modelData.Description != null)
            {
                document.Add(new Field(StronglyTyped.PropertyName<ProductSearchModel>(x => x.Description), modelData.Description, Field.Store.YES, Field.Index.ANALYZED,
                    Field.TermVector.WITH_POSITIONS_OFFSETS));
            }


            if (modelData.ImageName != null)
            {
                document.Add(new Field(StronglyTyped.PropertyName<ProductSearchModel>(x => x.ImageName), modelData.ImageName, Field.Store.YES, Field.Index.NOT_ANALYZED));
            }


            // add entry to index
            writer.AddDocument(document);
        }

        public static void AddUpdateLuceneIndex(IEnumerable<ProductSearchModel> modelData)
        {
            // init lucene
            var analyzer = new StandardAnalyzer(_version);
            using (var writer = new IndexWriter(_directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                // add data to lucene search index (replaces older entry if any)
                foreach (var data in modelData)
                    _addToLuceneIndex(data, writer);

                // close handles
                analyzer.Close();
                writer.Optimize();
                writer.Commit();
                writer.Dispose();
            }
        }

        public static void AddUpdateLuceneIndex(ProductSearchModel modelData)
        {
            AddUpdateLuceneIndex(new List<ProductSearchModel> { modelData });
        }

        public static void ClearLuceneIndexRecord(int projectId)
        {
            // init lucene
            var analyzer = new StandardAnalyzer(_version);
            using (var writer = new IndexWriter(_directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                var searchQuery = new TermQuery(new Term(StronglyTyped.PropertyName<ProductSearchModel>(x => x.Id), projectId.ToString(CultureInfo.InvariantCulture)));


                // remove older index entry
                writer.DeleteDocuments(searchQuery);

                // close handles
                analyzer.Close();
                writer.Dispose();
            }
        }


        public static bool ClearLuceneIndex()
        {
            try
            {
                var analyzer = new StandardAnalyzer(Version.LUCENE_30);
                using (var writer = new IndexWriter(_directory, analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED))
                {
                    // remove older index entries
                    writer.DeleteAll();

                    // close handles
                    analyzer.Close();
                    writer.Dispose();
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static void Optimize()
        {
            var analyzer = new StandardAnalyzer(Version.LUCENE_30);
            using (var writer = new IndexWriter(_directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                analyzer.Close();
                writer.Optimize();
                writer.Dispose();
            }
        }

        private static ProductSearchModel _mapLuceneDocumentToData(Document doc)
        {
            return new ProductSearchModel
            {
                Id = Convert.ToInt32(doc.Get(StronglyTyped.PropertyName<ProductSearchModel>(x => x.Id))),
                Description = doc.Get(StronglyTyped.PropertyName<ProductSearchModel>(x => x.Description)),
                Name = doc.Get(StronglyTyped.PropertyName<ProductSearchModel>(x => x.Name)),
                ImageName = doc.Get(StronglyTyped.PropertyName<ProductSearchModel>(x => x.ImageName))
            };
        }

        private static IEnumerable<ProductSearchModel> _mapLuceneToDataList(IEnumerable<Document> hits)
        {
            return hits.Select(_mapLuceneDocumentToData).ToList();
        }

        private static IEnumerable<ProductSearchModel> _mapLuceneToDataList(IEnumerable<ScoreDoc> hits,
            IndexSearcher searcher)
        {
            return hits.Select(hit => _mapLuceneDocumentToData(searcher.Doc(hit.Doc))).ToList();
        }

        private static Query parseQuery(string searchQuery, QueryParser parser)
        {
            Query query;
            try
            {
                query = parser.Parse(searchQuery.Trim());
            }
            catch (ParseException)
            {
                query = parser.Parse(QueryParser.Escape(searchQuery.Trim()));
            }
            return query;
        }

        private static IEnumerable<ProductSearchModel> _search(string searchQuery, string[] searchFields)
        {
            // validation
            if (string.IsNullOrEmpty(searchQuery.Replace("*", "").Replace("?", "")))
                return new List<ProductSearchModel>();

            // set up lucene searcher
            using (var searcher = new IndexSearcher(_directory, false))
            {
                const int hitsLimit = 1000;
                var analyzer = new StandardAnalyzer(Version.LUCENE_30);


                var parser = new MultiFieldQueryParser
                    (Version.LUCENE_30, searchFields, analyzer);
                Query query = parseQuery(searchQuery, parser);
                ScoreDoc[] hits = searcher.Search(query, null, hitsLimit, Sort.RELEVANCE).ScoreDocs;

                if (hits.Length == 0)
                {
                    searchQuery = searchByPartialWords(searchQuery);
                    query = parseQuery(searchQuery, parser);
                    hits = searcher.Search(query, hitsLimit).ScoreDocs;
                }

                IEnumerable<ProductSearchModel> results = _mapLuceneToDataList(hits, searcher);
                analyzer.Close();
                searcher.Dispose();
                return results;
            }
        }

        public static IEnumerable<ProductSearchModel> Search(string input, params string[] fieldsName)
        {
            if (string.IsNullOrEmpty(input))
                return new List<ProductSearchModel>();

            IEnumerable<string> terms = input.Trim().Replace("-", " ").Split(' ')
                .Where(x => !string.IsNullOrEmpty(x)).Select(x => x.Trim() + "*");
            input = string.Join(" ", terms);
            return _search(input, fieldsName);
        }

        public static IEnumerable<ProductSearchModel> SearchDefault(string input, string[] fieldsName)
        {
            return string.IsNullOrEmpty(input) ? new List<ProductSearchModel>() : _search(input, fieldsName);
        }

        public static IEnumerable<ProductSearchModel> GetAllIndexRecords()
        {
            // validate search index
            if (!Directory.EnumerateFiles(_luceneDir).Any())
                return new List<ProductSearchModel>();

            // set up lucene searcher
            var searcher = new IndexSearcher(_directory, false);
            IndexReader reader = IndexReader.Open(_directory, false);
            var docs = new List<Document>();
            TermDocs term = reader.TermDocs();
            while (term.Next()) docs.Add(searcher.Doc(term.Doc));
            reader.Dispose();
            searcher.Dispose();
            return _mapLuceneToDataList(docs);
        }

        private static string searchByPartialWords(string bodyTerm)
        {
            bodyTerm = bodyTerm.Replace("*", "").Replace("?", "");
            IEnumerable<string> terms = bodyTerm.Trim().Replace("-", " ").Split(' ')
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(x => x.Trim() + "*");
            bodyTerm = string.Join(" ", terms);
            return bodyTerm;
        }

        public static string[] SuggestSilmilarWords(string term, int count = 10)
        {
            IndexReader indexReader = IndexReader.Open(FSDirectory.Open(_luceneDir), true);

            // Create the SpellChecker
            var spellChecker = new SpellChecker.Net.Search.Spell.SpellChecker(FSDirectory.Open(_luceneDir + "\\Spell"));

            // Create SpellChecker Index
            spellChecker.ClearIndex();
            spellChecker.IndexDictionary(new LuceneDictionary(indexReader, StronglyTyped.PropertyName<ProductSearchModel>(x => x.Name)));
            spellChecker.IndexDictionary(new LuceneDictionary(indexReader, StronglyTyped.PropertyName<ProductSearchModel>(x => x.Description)));

            //Suggest Similar Words
            return spellChecker.SuggestSimilar(term, count, null, null, true);
        }

    }
}
