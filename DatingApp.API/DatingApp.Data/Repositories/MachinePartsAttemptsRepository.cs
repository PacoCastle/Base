using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DatingApp.Core.Models;
using DatingApp.Core.Repositories;
using Microsoft.Data.SqlClient;
using System;

namespace DatingApp.Data.Repositories
{
    public class MachinePartsAttemptsRepository : Repository<MachinePartAttempt>, IMachinePartsAttemptsRepository
    {
        private readonly UDataContext _context;
        public MachinePartsAttemptsRepository(UDataContext context) 
            : base(context)
        { 
            _context = context;
        }  

        public async Task<int> AddByStored(string MachineModel, string PartModel, string InternalSequence)
        {
            int res = 0;
            try
            {
                var pMachineModel = new SqlParameter
                {
                    ParameterName = "MachineModel",
                    DbType = System.Data.DbType.String,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = MachineModel
                };
                var pPartModel = new SqlParameter
                {
                    ParameterName = "PartModel",
                    DbType = System.Data.DbType.String,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = PartModel
                };
                var pInternalSequence = new SqlParameter
                {
                    ParameterName = "InternalSequence",
                    DbType = System.Data.DbType.String,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = InternalSequence
                };
                var pSuccessful = new SqlParameter
                {
                    ParameterName = "Successful",
                    DbType = System.Data.DbType.Boolean,
                    Direction = System.Data.ParameterDirection.Output
                };

                var pId = new SqlParameter
                {
                    ParameterName = "Id",
                    DbType = System.Data.DbType.Int32,
                    Direction = System.Data.ParameterDirection.Output,
                    Value = 0
                };

                var pMessage = new SqlParameter
                {
                    ParameterName = "Message",
                    DbType = System.Data.DbType.String,
                    Direction = System.Data.ParameterDirection.Output,
                    Size = 250,
                    Value = -1
                    
                };

                var sql = "exec sp_MachinePartAttempt_Insert @MachineModel ,@PartModel ,@InternalSequence  ,@Successful OUTPUT ,@Id OUTPUT ,@Message OUTPUT";
                var result = _context.Database.ExecuteSqlCommand(sql, pMachineModel, pPartModel, pInternalSequence, pSuccessful, pId, pMessage);

                var resSuccessful = pSuccessful.Value;
                var resId = pId.Value;
                res = (int) resId;
                var resMessage = pMessage.Value;    
            }
            catch (Exception ex )
            {
                
                var exe =  ex.Message;
            }
                     

            return res;             
                
        }  
        
    }
}

