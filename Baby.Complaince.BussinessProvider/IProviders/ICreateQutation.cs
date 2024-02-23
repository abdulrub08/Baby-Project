using Baby.Complaince.Entities.QutationRepositry;
using Baby.Complaince.Entities.ResponseModel;
using Baby.Complaince.Entities.Vendor;
using System.Collections.Generic;
using System.Net.Mail;

namespace Baby.Complaince.BussinessProvider.IProviders
{
    public interface ICreateQutation
    {
        DbOutput RequestQutation(Qutation qutation);
        List<string> GetAllVendors();
    }
}
