using DatingApp.Core;
using DatingApp.Core.Models;
using DatingApp.Core.Repositories;
using DatingApp.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/*
The PartService class
Contains all Bussines Logic can be Injected many repositories that you considered necesary 
for make validations, rules, etc. Can declare specifies methods and functions 
declaring names and parameters that can be same of the IPartServiceRepository declarations
*/
/// <summary>
/// The PartService class
/// Contains all Bussines Logic can be Injected many repositories that you considered necesary 
/// for make validations, rules, etc. Can declare specifies methods and functions 
/// declaring names and parameters that can be same of the IPartServiceRepository declarations
/// </summary> 
/// 


namespace DatingApp.Services
{
    public class PartService : IPartService
    {
        private readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// PartService receive a IUnitOfWork Interface that encapsulates all Names of the All repositories
        /// that the Data Layer has for Data Base Operations. 
        /// </summary>
        /// <param name="unitOfWork">The Interface IUnitOfWork </param>   
        public PartService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Get the first or Default PartModel Register filter by Id 
        /// </summary>
        /// <param name="id">Id of PartModel for the search</param>
        /// <returns>BaseResponse<PartModel> with the result of the search By Id</returns>
        public async Task<BaseResponse<PartModel>> GetPartById(int id)
        {
            //Declare variables for result and errors for be filled with the response of _unitOfWork
            BaseResponse<PartModel> result = new BaseResponse<PartModel>();
            List<string> err = new List<string>();

            try
            {
                //Using _unitOfWork call to a PartRepository and through of the 
                //GetByIdAsync GENERIC method Search the first or Default
                result.DataResponse = await _unitOfWork.PartRepository.GetByIdAsync(id);

                //If the Query was Successful then in the result this flat in true
                result.Successful = true; 
            }
            catch (System.Exception ex )
            {
                //If exist a Exception it's catched and Set in err List the Message 
                err.Add("Error en PartService -> GetPartById " + ex.Message);
                //Set in the result errors object the exception message
                result.errors = err;
            }          

            return result; 
        }
        /// <summary>
        /// Get All registers of  PartModel Register 
        /// </summary>
        /// <returns>BaseResponse<IEnumerable<PartModel>> object with result of the search All </returns>
        public async Task<BaseResponse<IEnumerable<PartModel>>> GetParts()
        {
            //Declare variables for result and errors for be filled with the response of _unitOfWork
            BaseResponse<IEnumerable<PartModel>> result = new BaseResponse<IEnumerable<PartModel>>();
            List<string> err = new List<string>();

            try
            {
                //Using _unitOfWork call to a PartRepository and through of the 
                //GetAllAsync GENERIC method Search All register
                result.DataResponse = await  _unitOfWork.PartRepository.GetAllAsync(); 

                //If the Query was Successful then in the result this flat in true  
                result.Successful = true; 
            }
            catch (System.Exception ex )
            {
                //If exist a Exception it's catched and Set in err List the Message 
                err.Add("Error en PartService -> GetParts " + ex.Message);
                //Set in the result errors object the exception message
                result.errors = err;
            }          

            return result;
        }
        /// <summary>
        /// Function thah Create a register for PartModel  
        /// </summary>
        /// <param name="PartToBeCreatedModel">PartModel for be inserted in the table<</param>        
        /// <returns>Task<BaseResponse<PartModel>>  result object with the response from Repository Funtion AddAsync</returns>
        public async Task<BaseResponse<PartModel>> CreatePart(PartModel PartToBeCreatedModel)
        {
            //Declare variables for result and errors for be filled with the response of _unitOfWork
            BaseResponse<PartModel> result = new BaseResponse<PartModel>();
            List<string> err = new List<string>();
            List<DetailResponse> detailResponse = new  List<DetailResponse>();

            try
            {
                await _unitOfWork.PartRepository.AddAsync(PartToBeCreatedModel);
                await _unitOfWork.CommitAsync();

                //Set Successful in true because the commit was completed     
                result.Successful = true;

                //Set in Data Response of result object   
                result.DataResponse = await _unitOfWork.PartRepository.GetByIdAsync( PartToBeCreatedModel.Id);

                //Set in Details local variable  object a message for successful execution in the Create
                detailResponse.Add(result.AddDetailResponse (PartToBeCreatedModel.Id, "Regitro exitoso"));

                //Set Details from local variable to result before return
                result.Details = detailResponse;

            }
            catch (System.Exception ex )
            {
                //If exist a Exception it's catched and Set in err List the Message 
                err.Add("Error en PartService -> CreatePart " + ex.Message);
                //Set in the result errors object the exception message
                result.errors = err;
            }          

            return result;
        }
        /// <summary>
        /// Function thah Update a register for PartModel  
        /// </summary>
        /// <param name="PartToBeUpdateModel">Instance of PartModel for be Updated</param>
        /// <param name="PartForUpdateModel">Instance of PartModel with the Data to Update</param>
        /// <returns></returns>
        public async Task<BaseResponse<PartModel>>  UpdatePart(PartModel PartToBeUpdateModel , PartModel PartForUpdateModel)
        {

            //Declare variables for result and errors for be filled with the response of _unitOfWork
            BaseResponse<PartModel> result = new BaseResponse<PartModel>();
            List<string> err = new List<string>(); 
            List<DetailResponse> detailResponse = new  List<DetailResponse>();

            try
            {
                //Set in the object TO BE UPDATED (ORIGIN) the changues from object FOR UPDATE                
                PartToBeUpdateModel.Name = PartForUpdateModel.Name;
                PartToBeUpdateModel.Description = PartForUpdateModel.Description;
                PartToBeUpdateModel.Status = PartForUpdateModel.Status;
            

                //Call commit of the changues in the past step            
                await _unitOfWork.CommitAsync();  

                //Set Successful in true because the commit was completed     
                result.Successful = true;

                //Set in Data Response of result object   
                result.DataResponse = await _unitOfWork.PartRepository.GetByIdAsync(PartToBeUpdateModel.Id);

                //Set in Details local variable  object a message for successful execution in the Update
                detailResponse.Add(result.AddDetailResponse (PartToBeUpdateModel.Id, "Actualización realizada correctamente"));

                //Set Details from local variable to result before return
                result.Details = detailResponse;
                
            }
            catch (System.Exception ex )
            {
                //If exist a Error un the transaction then set Error Detail for return in the result
                err.Add("Error en PartService -> UpdatePartModel " + ex.Message);
                result.errors = err;                
            }

            //return result FROM SERVICE object
            return result;  
            
        }
        /// <summary>
        /// Get the first or Default PartModel Register filter by Name 
        /// </summary>
        /// <param name="id">Id of PartModel for the search</param>
        /// <returns>BaseResponse<PartModel> with the result of the search By Name</returns>
        public async Task<BaseResponse<PartModel>>  GetPartByName(string name)
        {
            //Declare variables for result and errors for be filled with the response of _unitOfWork
            BaseResponse<PartModel> result = new BaseResponse<PartModel>();
            List<string> err = new List<string>();

            try
            {
                //Using _unitOfWork call to a PartRepository and through of the 
                //GetByIdAsync GENERIC method Search the first or Default
                result.DataResponse = await _unitOfWork.PartRepository.GetPartByName(name);

                //If the Query was Successful then in the result this flat in true
                result.Successful = true; 
            }
            catch (System.Exception ex )
            {
                //If exist a Exception it's catched and Set in err List the Message 
                err.Add("Error en PartService -> GetPartModel " + ex.Message);
                //Set in the result errors object the exception message
                result.errors = err;
            }          

            return result;
            
        }
    }
}
