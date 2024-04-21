using SharedKernel.Domain.DomainModel.ProductModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMgmtSlices.Repository.CategoryIHierarchyMappers
{
    public class HierarchyTableMappers : IHierarchyTableMappers
    {
        public Dictionary<string, object> CreateMapForInsert(CategoryHierarchy hierarchy)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> CreateMapForUpdate(CategoryHierarchy hierarchy)
        {
            throw new NotImplementedException();
        }
    }
}
