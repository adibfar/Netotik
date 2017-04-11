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
    public class ResourceService : BaseService<Netotik.Domain.Entity.Resource>, IResourceService
    {

        private readonly IMappingEngine _mappingEngine;
        public ResourceService(IMappingEngine mappingEngine, IUnitOfWork unit)
            : base(unit)
        {
            _mappingEngine = mappingEngine;
        }

        public IList<ResourceItem> GetList(RequestListModel model, out long TotalCount, out long ShowCount)
        {
            IQueryable<Resource> all = dbSet.AsQueryable();
            TotalCount = all.LongCount();

            // Apply Filtering
            if (!string.IsNullOrEmpty(model.sSearch))
            {
                all = all.
                    Where(x => x.Name.Contains(model.sSearch) || x.Value.Contains(model.sSearch))
                    .AsQueryable();
            }


            // Apply Sorting
            Func<Resource, string> orderingFunction = (x => model.iSortCol_0 == 1 ? x.Name :
                                                            model.iSortCol_0 == 2 ? x.Value : x.Culture);
            // asc or desc
            all = model.sSortDir_0 == "asc" ? all.OrderBy(orderingFunction).AsQueryable() : all.OrderByDescending(orderingFunction).AsQueryable();

            ShowCount = all.Count();
            return all.AsEnumerable().Skip(model.iDisplayStart).Take(model.iDisplayLength)
                .Select((x, index) => new ResourceItem { Key = x.Name, Value = x.Value, culture = x.Culture, RowNumber = model.iDisplayStart + index + 1 })
                .ToList();

        }

        public void SeedDataBase(string xmlRsourceString, string culture)
        {
            if (!dbSet.Any())
            {

                var list = XDocument.Parse(xmlRsourceString)
                    .Element("Language")
                    .Elements("LocaleResource")
                    .Select(e => new Netotik.Domain.Entity.Resource
                    {
                        Name = e.Attribute("Name").Value,
                        Value = e.Value,
                        Culture = culture
                    });

                UnitOfWork.AddThisRange<Netotik.Domain.Entity.Resource>(list);
                UnitOfWork.SaveAllChanges();
            }

        }
    }
}
