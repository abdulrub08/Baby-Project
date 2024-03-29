﻿using Baby.Complaince.BussinessProvider.IProviders;
using Baby.Complaince.Entities.QutationRepositry;
using Baby.Complaince.Entities.ResponseModel;
using Baby.Complaince.Entities.Vendor;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Net.Mail;

namespace Baby.Complaince.DataAccess.Repository
{
    public class VendorService : BaseDAL, IVendorService
    {
        private Database _dbContextDQCPRDDB;

        public VendorService()
        {
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            _dbContextDQCPRDDB = factory.Create(C_Connection_Big);
        }
        public DbOutput RequestVendor(ref Vendor vendor)
        {
            using (DbCommand command = _dbContextDQCPRDDB.GetStoredProcCommand(DBConstraints.INSERT_VENDOR_Qutation))
            {
                _dbContextDQCPRDDB.AddInParameter(command, "ProductID", DbType.Int64, vendor.ProductID);
                _dbContextDQCPRDDB.AddInParameter(command, "VendorID", DbType.Int64, vendor.VendorID);
                _dbContextDQCPRDDB.AddInParameter(command, "CustomerEmailID", DbType.String, vendor.CustomerEmailID);
                _dbContextDQCPRDDB.AddInParameter(command, "CustomerID", DbType.Int64, vendor.CustomerID);
                _dbContextDQCPRDDB.AddInParameter(command, "DefaultPrice", DbType.Decimal, vendor.Price);
                _dbContextDQCPRDDB.AddInParameter(command, "Stock", DbType.Int64, vendor.Stock);
                _dbContextDQCPRDDB.AddInParameter(command, "Remark", DbType.String, vendor.Description);
                _dbContextDQCPRDDB.AddInParameter(command, "CreatedBy", DbType.String, vendor.CreatedBy);

                _dbContextDQCPRDDB.AddOutParameter(command, "Code", DbType.String, 4000);
                _dbContextDQCPRDDB.AddOutParameter(command, "Message", DbType.String, 4000);
                _dbContextDQCPRDDB.AddOutParameter(command, "VendorTransactionID", DbType.Int32, 4000);
                _dbContextDQCPRDDB.ExecuteNonQuery(command);
                vendor.Id = Convert.ToInt32(_dbContextDQCPRDDB.GetParameterValue(command, "VendorTransactionID"));
                return new DbOutput()
                {
                    Code = Convert.ToInt32(_dbContextDQCPRDDB.GetParameterValue(command, "Code")),
                    Message =  Convert.ToString(_dbContextDQCPRDDB.GetParameterValue(command, "Message"))
                };
            }
        }
        public List<string> GetAllVendors()
        {
            List<string> vendorList = new List<string>();
            using (DbCommand command = _dbContextDQCPRDDB.GetStoredProcCommand(DBConstraints.GET_ALL_Vendor_Email))
            {
                using (IDataReader reader = _dbContextDQCPRDDB.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        vendorList.Add(GenerateVendor(reader));
                    }
                }
            }
            return vendorList;
        }
        public Customer GetCustomerTransaction(int id)
        {
            Customer _cust = new Customer();
            using (DbCommand command = _dbContextDQCPRDDB.GetStoredProcCommand(DBConstraints.GET_Customer_Transaction))
            {
                _dbContextDQCPRDDB.AddInParameter(command, "ID", DbType.Int64, id);
                using (IDataReader reader = _dbContextDQCPRDDB.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        _cust.email = GetStringFromDataReader(reader, "CustomerEmailID");
                        _cust.id = GetIntegerFromDataReader(reader, "CustomerID");
                    }
                }
            }
            return _cust;
        }
        private string GenerateVendor(IDataReader reader)
        {
            return GetStringFromDataReader(reader, "ContactEmail");

        }
    }
}
