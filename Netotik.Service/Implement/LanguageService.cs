using Netotik.Common.Security;
using Netotik.Data;
using Netotik.Domain.Entity;
using Netotik.Services.Abstract;
using Netotik.Services.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Netotik.Common.DataTables;
using Netotik.ViewModels.Common.Resource;
using AutoMapper;

namespace Netotik.Services.Implement
{
    public class LanguageService : BaseService<Netotik.Domain.Entity.Language>, ILanguageService
    {

        private readonly IMappingEngine _mappingEngine;
        public LanguageService(IMappingEngine mappingEngine, IUnitOfWork unit)
            : base(unit)
        {
            _mappingEngine = mappingEngine;
        }

        public IList<LanguageItem> GetList(RequestListModel model, out long TotalCount, out long ShowCount)
        {
            IQueryable<Language> all = dbSet.AsQueryable();
            TotalCount = all.LongCount();

            // Apply Filtering
            if (!string.IsNullOrEmpty(model.sSearch))
            {
                all = all.
                    Where(x => x.Name.Contains(model.sSearch) || x.LanguageCulture.Contains(model.sSearch)
                    || x.UniqueSeoCode.Contains(model.sSearch))
                    .AsQueryable();
            }


            ShowCount = all.Count();
            return all.OrderBy(x => x.DisplayOrder).AsEnumerable().Skip(model.iDisplayStart).Take(model.iDisplayLength)
                .Select((x, index) => new LanguageItem
                {
                    Name = x.Name,
                    LanguageCulture = x.LanguageCulture,
                    UniqueSeoCode = x.UniqueSeoCode,
                    Rtl = x.Rtl,
                    FlagImageFileName = x.FlagImageFileName,
                    DisplayOrder = x.DisplayOrder,
                    Id = x.Id,
                    Published = x.Published,
                    RowNumber = model.iDisplayStart + index + 1
                })
                .ToList();

        }

        public void SeedDataBase(string xmlResource)
        {
            if (!dbSet.Any())
            {

                var list = XDocument.Parse(xmlResource)
                    .Element("Language")
                    .Elements("LocaleResource")
                    .Select(e => new Netotik.Domain.Entity.LocaleStringResource
                    {
                        Name = e.Attribute("Name").Value,
                        Value = e.Value,
                    });

                var lang = new Language()
                {
                    Name = "Persian",
                    DisplayOrder = 0,
                    FlagImageFileName = "ir.png",
                    LanguageCulture = "en-us",
                    Published = true,
                    Rtl = true,
                    UniqueSeoCode = "Persian",
                    LocaleStringResources = list.ToList()
                };
                dbSet.Add(lang);
                UnitOfWork.SaveAllChanges();
            }

        }
    }
}
