using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DatingApp.Core.Models;
using DatingApp.Core.Repositories;
using Microsoft.Data.SqlClient;
using System;

/*
The MachinePartsAttemptsRepository class
Contains all Data Base Funtionality for MachinePartsAttempts 
using Repository for gral operations and can declare specifies methods and functions 
declaring names and parameters that can be same of the IMachinePartsAttemptsRepository declarations
*/
/// <summary>
/// The MachinePartsAttemptsRepository class
/// Contains all Data Base Funtionality for MachinePartsAttempts 
/// using Repository for gral operations and can declare specifies methods and functions 
/// declaring names and parameters that can be same of the IMachinePartsAttemptsRepository declarations
/// </summary>    
/// 

namespace DatingApp.Data.Repositories
{
    public class MachinePartsAttemptsRepository : Repository<MachinePartAttempt>, IMachinePartsAttemptsRepository
    {
        private readonly UDataContext _context;

        /// <summary>
        /// The constructor of MachinePartsAttemptsRepository receive a UDataContext
        /// that is used for operations with Data Base
        /// </summary>
        /// <param name="context"></param>
        public MachinePartsAttemptsRepository(UDataContext context) 
            : base(context)
        { 
            _context = context;
        }  
        /// <summary>
        /// Create a MachinePartAttempt register using Stored Procedured.
        /// </summary>
        /// <param name="MachineModel">Machine for be inserted in the table</param>
        /// <param name="PartModel">Part for be inserted in the table</param>
        /// <param name="InternalSequence">Sequence for be inserted in the table</param>
        /// <returns>
        /// BaseResponse<MachinePartAttempt> object with the response from Stored Procedured
        /// </returns>
        public async Task<BaseResponse<MachinePartAttempt>> AddByStored(string MachineModel, string PartModel, string InternalSequence)
        {
            //Object for return the result of invoque the Stored Procedured
            BaseResponse<MachinePartAttempt> result = new BaseResponse<MachinePartAttempt>();

            
            try
            {
                //Input and OutPut SqlParameter parameters for send to Stored Procedured.
                //Input Type is for send data and Output Type are for receive it.
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

                //Set the name of the Stored Procedured and Parameters for be invoqued
                var sql = "exec sp_MachinePartAttempt_Insert @MachineModel ,@PartModel ,@InternalSequence  ,@Successful OUTPUT ,@Id OUTPUT ,@Message OUTPUT";

                //Execute the Sentence sending Input parameters and receiving Output response
                _context.Database.ExecuteSqlCommand(sql, pMachineModel, pPartModel, pInternalSequence, pSuccessful, pId, pMessage);

                //Set in Successful property the OutPut tha indicate if the operation was Successful can be true or false
                result.Successful = (bool) pSuccessful.Value;

                //Validate if response was Successful then set in Details List the ScopedId Returned and the message
                if(result.Successful)
                {
                    List<DetailResponse> list = new  List<DetailResponse>();
                    list.Add(result.AddDetailResponse ((int)pId.Value, (string)pMessage.Value));
                    result.Details= list;      
                }
                //Validate if response not was Successful then set in error List the Id and message for the error
                //returned in OutPut Parameters
                else
                {
                    result.errors.Add("Error -> MachinePartsAttemptsRepository ->  AddByStored " + (int)pId.Value +  " " + (string)pMessage.Value);   
                }                         
                
            }
            catch (Exception ex )
            {
                //Validate if the execution with DataBase was not be completed then set Successful = false
                //and return the error Catched and Add it in Error list 
                result.Successful = false;
                result.errors.Add("Error -> MachinePartsAttemptsRepository ->  AddByStored " + ex.HResult +  " " + ex.Message.ToString());                
            }                 

            //Return result to a Service thah invoque this Function            
            return result;             
                
        }  
        public async Task<BaseResponse<MachinePartAttempt>> GetByMachinePartSequenceName(string pMachineModel, string pPartModel, string pInternalSequence)
        {
            //Object for return the result of invoque the Stored Procedured
            BaseResponse<MachinePartAttempt> result = new BaseResponse<MachinePartAttempt>();
            
            IQueryable<MachinePartAttempt> MachinePartAttemptFromRepo = null;

            MachinePartAttemptFromRepo = from mpa in _context.MachinePartAttempt
                                      join machine in _context.MachineModel on mpa.Id equals machine.Id
                                      join part in _context.PartModel on mpa.Id equals part.Id
                                      where machine.Name == pMachineModel && part.Name == pPartModel 
                                      && mpa.InternalSequence == pInternalSequence
                                      select mpa;
                result.DataResponse = (MachinePartAttempt) MachinePartAttemptFromRepo.FirstOrDefault();

            return result;

        }
    }
}
