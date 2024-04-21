using Core.Domain.DomainModel.ProductModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMgmtSlices.Repository.CategoryIHierarchyMappers
{
    public interface IHierarchyTableMappers
    {
        Dictionary<string, object> CreateMapForInsert(CategoryHierarchy hierarchy);

        Dictionary<string, object> CreateMapForUpdate(CategoryHierarchy hierarchy);
    }
}
