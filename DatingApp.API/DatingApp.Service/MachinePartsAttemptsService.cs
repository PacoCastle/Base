using System;
using System.Collections.Generic;
using System.Threading.Tasks;
//using DatingApp.Api.Data;
using DatingApp.Core;
using DatingApp.Core.Models;
using DatingApp.Core.Repositories;
using DatingApp.Core.Services;
/*
The MachinePartsAttemptsService class
Contains all Bussines Logic can be Injected many repositories that you considered necesary 
for make validations, rules, etc. Can declare specifies methods and functions 
declaring names and parameters that can be same of the IMachinePartsAttemptsRepository declarations
*/
/// <summary>
/// The MachinePartsAttemptsService class
/// Contains all Bussines Logic can be Injected many repositories that you considered necesary 
/// for make validations, rules, etc. Can declare specifies methods and functions 
/// declaring names and parameters that can be same of the IMachinePartsAttemptsRepository declarations
/// </summary> 
/// 

namespace DatingApp.Services
{
    public class MachinePartsAttemptsService : IMachinePartsAttemptsService
    {
        private readonly IUnitOfWork _unitOfWork;     
        
        /// <summary>
        /// MachinePartsAttemptsService receive a IUnitOfWork Interface that encapsulates all Names of the All repositories
        /// that the Data Layer has for Data Base Operations. 
        /// </summary>
        /// <param name="unitOfWork">The Interface IUnitOfWork </param>   
        public MachinePartsAttemptsService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;            
        }
        /// <summary>
        /// Get the first or Default MachinePartAttempt Register filter by Id 
        /// </summary>
        /// <param name="id">Id of MachinePartAttempt for the search</param>
        /// <returns>BaseResponse<MachinePartAttempt> with the result of the search By Id</returns>
        public async Task<BaseResponse<MachinePartAttempt>> GetMachinePartAttempt(int id)
        {
            //Declare variables for result and errors for be filled with the response of _unitOfWork
            BaseResponse<MachinePartAttempt> result = new BaseResponse<MachinePartAttempt>();
            List<string> err = new List<string>();

            try
            {
                //Using _unitOfWork call to a MachinePartsAttemptsRepository and through of the 
                //GetByIdAsync GENERIC method Search the first or Default
                result.DataResponse = await _unitOfWork.MachinePartsAttemptsRepository.GetByIdAsync(id);

                //If the Query was Successful then in the result this flat in true
                result.Successful = true; 
            }
            catch (System.Exception ex )
            {
                //If exist a Exception it's catched and Set in err List the Message 
                err.Add("Error en MachinePartsAttemptsService -> GetMachinePartAttempt " + ex.Message);
                //Set in the result errors object the exception message
                result.errors = err;
            }          

            return result; 
                    
        }
        /// <summary>
        /// Get All registers of  MachinePartAttempt Register 
        /// </summary>
        /// <returns>BaseResponse<IEnumerable<MachinePartAttempt>> object with result of the search All </returns>
        public async Task<BaseResponse<IEnumerable<MachinePartAttempt>>> GetMachinePartsAttempts()
        {
            //Declare variables for result and errors for be filled with the response of _unitOfWork
            BaseResponse<IEnumerable<MachinePartAttempt>> result = new BaseResponse<IEnumerable<MachinePartAttempt>>();
            List<string> err = new List<string>();

            try
            {
                //Using _unitOfWork call to a MachinePartsAttemptsRepository and through of the 
                //GetAllAsync GENERIC method Search All register
                result.DataResponse = await  _unitOfWork.MachinePartsAttemptsRepository.GetAllAsync(); 

                //If the Query was Successful then in the result this flat in true  
                result.Successful = true; 
            }
            catch (System.Exception ex )
            {
                //If exist a Exception it's catched and Set in err List the Message 
                err.Add("Error en MachinePartsAttemptsService -> GetMachinePartAttempt " + ex.Message);
                //Set in the result errors object the exception message
                result.errors = err;
            }          

            return result;
        }  
        /// <summary>
        /// Function thah Create a register for MachinePartAttempt  
        /// </summary>
        /// <param name="MachineModel">Machine for be inserted in the table<</param>
        /// <param name="PartModel">Part for be inserted in the table</param>
        /// <param name="InternalSequence">Sequence for be inserted in the table</param>
        /// <returns>Task<BaseResponse<MachinePartAttempt>>  result object with the response from Repository Funtion AddByStored</returns>
        public async Task<BaseResponse<MachinePartAttempt>> AddByStored(string MachineModel, string PartModel, string InternalSequence)
        {
            //Declare variables for result and errors for be filled with the response of _unitOfWork
            BaseResponse<MachinePartAttempt> result = new BaseResponse<MachinePartAttempt>();
            List<string> err = new List<string>(); 

            try
            {
                //Search if the MachinePartAttempt exist in the DataBase filter by MachineModel, PartModel and InternalSequence
                var MachinePartAttemptFromRepo = await _unitOfWork.MachinePartsAttemptsRepository.GetByMachinePartSequenceName(MachineModel, PartModel, InternalSequence);
                
                //If the combination of the last search has regiters then return the error and exit for the routine
                if(MachinePartAttemptFromRepo != null)
                {
                    err.Add("La maquina - Parte - Id Interno se encuentran registrados");
                    result.errors = err;
                    return result;      
                }
                //Search if the Machine Name exist in the DataBase using MachineRepository 
                var MachineFromRepo = await _unitOfWork.MachineRepository.GetMachineByName(MachineModel);

                //If the search not return Data then Add this Error to the List
                if (MachineFromRepo == null)
                {
                    err.Add("La Maquina ingresada no existe en el catálogo");                
                }

                //Search if the Machine Name exist in the DataBase using PartRepository
                var PartFromRepo = await _unitOfWork.PartRepository.GetPartByName(PartModel);

                //If the search not return Data then Add this Error to the List
                if (PartFromRepo == null)
                {
                    err.Add("La Pieza ingresada no existe en el catálogo"); 
                }

                // Set in the result errors the information obtained for search Machine and Part 
                result.errors = err;

                //Also Can try Save if the Machine and Part Exist in the DataBase
                if (MachineFromRepo != null && PartFromRepo != null)
                {
                    //Call service for Create register in MachinePartAttempt 
                    var repoResponse = _unitOfWork.MachinePartsAttemptsRepository.AddByStored(MachineModel, PartModel, InternalSequence); 

                    //If result get in true Sucessful the register was Created
                    if(repoResponse.Result.Successful)
                    {
                        //Search the register Created for return it in the DataResponse and show in the controller response
                        repoResponse.Result.DataResponse = await _unitOfWork.MachinePartsAttemptsRepository.GetByIdAsync(repoResponse.Result.Details[0].Id);
                    } 
                    //set in the result object OF THE SERVICE the result FROM THE REPOSITORY
                    result = repoResponse.Result;
                }
            }
            catch (System.Exception ex )
            {
                //If exist a Error un the transaction then set Error Detail for return in the result
                err.Add("Error en MachinePartsAttemptsService -> AddByStored " + ex.Message);
                result.errors = err;
            } 
            //return result FROM SERVICE object
            return result;
            
        }
        /// <summary>
        /// Function thah Update a register for MachinePartAttempt  
        /// </summary>
        /// <param name="MachPartAttemToBeUpdate">Instance of MachinePartAttempt for be Updated</param>
        /// <param name="MachinePartAttemptForUpdate">Instance of MachinePartAttempt with the Data to Update</param>
        /// <returns></returns>
        public async Task<BaseResponse<MachinePartAttempt>> UpdateMachinePartAttempt(MachinePartAttempt MachPartAttemToBeUpdate, MachinePartAttempt MachinePartAttemptForUpdate)
        {
            //Declare variables for result and errors for be filled with the response of _unitOfWork
            BaseResponse<MachinePartAttempt> result = new BaseResponse<MachinePartAttempt>();
            List<string> err = new List<string>(); 
            List<DetailResponse> detailResponse = new  List<DetailResponse>();

            try
            {
                //Set in the object TO BE UPDATED (ORIGIN) the changues from object FOR UPDATE                
                MachPartAttemToBeUpdate.AvailableAttempts = MachinePartAttemptForUpdate.AvailableAttempts;            

                //Call commit of the changues in the past step            
                await _unitOfWork.CommitAsync();

                //Set Successful in true because the commit was completed     
                result.Successful = true;

                //Set in Data Response of result object   
                result.DataResponse = await _unitOfWork.MachinePartsAttemptsRepository.GetByIdAsync( MachPartAttemToBeUpdate.Id);

                //Set in Details local variable  object a message for successful execution in the Update
                detailResponse.Add(result.AddDetailResponse (MachPartAttemToBeUpdate.Id, "Actualización realizada correctamente"));

                //Set Details from local variable to result before return
                result.Details = detailResponse;

                
            }
            catch (System.Exception ex )
            {
                //If exist a Error un the transaction then set Error Detail for return in the result
                err.Add("Error en MachinePartsAttemptsService -> UpdateMachinePartAttempt " + ex.Message);
                result.errors = err;                
            }  

            //return result FROM SERVICE object
            return result;         
        }
    }
}
