using Baby.Complaince.Entities.QutationRepositry;
using Baby.Complaince.Entities.ResponseModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Baby.Complaince.BussinessProvider.IProviders
{
    public interface IVendorService
    {
        DbOutput RequestVendor(ref Vendor vendor);
        List<string> GetAllVendors();
        Customer GetCustomerTransaction(int id);
    }
}
