using DatingApp.Core;
using DatingApp.Core.Models;
using DatingApp.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/*
The RoleService class
Contains all Bussines Logic can be Injected many repositories that you considered necesary 
for make validations, rules, etc. Can declare specifies methods and functions 
declaring names and parameters that can be same of the IRoleServiceRepository declarations
*/
/// <summary>
/// The RoleService class
/// Contains all Bussines Logic can be Injected many repositories that you considered necesary 
/// for make validations, rules, etc. Can declare specifies methods and functions 
/// declaring names and parameters that can be same of the IRoleServiceRepository declarations
/// </summary> 
/// 


namespace DatingApp.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// RoleService receive a IUnitOfWork Interface that encapsulates all Names of the All repositories
        /// that the Data Layer has for Data Base Operations. 
        /// </summary>
        /// <param name="unitOfWork">The Interface IUnitOfWork </param>   
        public RoleService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Get the first or Default Role Register filter by Name 
        /// </summary>
        /// <param name="id">Id of Role for the search</param>
        /// <returns>BaseResponse<Role> with the result of the search By Id</returns>
        public async Task<BaseResponse<Role>> GetRoleById(int id)
        {
            //Declare variables for result and errors for be filled with the response of _unitOfWork
            BaseResponse<Role> result = new BaseResponse<Role>();
            List<string> err = new List<string>();

            try
            {
                //Using _unitOfWork call to a RoleRepository and through of the 
                //GetRoleByName GENERIC method Search the first or Default
                result.DataResponse = await _unitOfWork.RoleRepository.GetByIdAsync(id);

                //If the Query was Successful then in the result this flat in true
                result.Successful = true; 
            }
            catch (System.Exception ex )
            {
                //If exist a Exception it's catched and Set in err List the Message 
                err.Add("Error en RoleService -> GetRoleById " + ex.Message);
                //Set in the result errors object the exception message
                result.errors = err;
            }          

            return result; 
        } 
        /// <summary>
        /// Get the first or Default Role Register filter by Name 
        /// </summary>
        /// <param name="id">Id of Role for the search</param>
        /// <returns>BaseResponse<Role> with the result of the search By Id</returns>
        public async Task<BaseResponse<String>> GetRoleByName(String name)
        {
            //Declare variables for result and errors for be filled with the response of _unitOfWork
            BaseResponse<String> result = new BaseResponse<String>();
            List<string> err = new List<string>();

            try
            {
                //Using _unitOfWork call to a RoleRepository and through of the 
                //GetRoleByName GENERIC method Search the first or Default
                result.DataResponse = await _unitOfWork.RoleRepository.GetRoleByName(name);

                //If the Query was Successful then in the result this flat in true
                result.Successful = true; 
            }
            catch (System.Exception ex )
            {
                //If exist a Exception it's catched and Set in err List the Message 
                err.Add("Error en RoleService -> GetRoleByName " + ex.Message);
                //Set in the result errors object the exception message
                result.errors = err;
            }          

            return result; 
        } 
        /// <summary>
        /// Get All registers of  Role Register 
        /// </summary>
        /// <returns>BaseResponse<IEnumerable<Role>> object with result of the search All </returns>
        public async Task<BaseResponse<IEnumerable<String>>> GetRoles()
        {
            //Declare variables for result and errors for be filled with the response of _unitOfWork
            BaseResponse<IEnumerable<String>> result = new BaseResponse<IEnumerable<String>>();
            List<string> err = new List<string>();

            try
            {
                //Using _unitOfWork call to a RoleRepository and through of the 
                //GetAllAsync GENERIC method Search All register
                result.DataResponse = await  _unitOfWork.RoleRepository.GetRoles(); 

                //If the Query was Successful then in the result this flat in true  
                result.Successful = true; 
            }
            catch (System.Exception ex )
            {
                //If exist a Exception it's catched and Set in err List the Message 
                err.Add("Error en RoleService -> GetRoles " + ex.Message);
                //Set in the result errors object the exception message
                result.errors = err;
            }          

            return result;
        }
        /// <summary>
        /// Function thah Create a register for Role  
        /// </summary>
        /// <param name="RoleToBeCreatedModel">Role for be inserted in the table<</param>        
        /// <returns>Task<BaseResponse<Role>>  result object with the response from Repository Funtion AddAsync</returns>
        public async Task<BaseResponse<Role>> CreateRole(Role RoleToBeCreatedModel)
        {
            //Declare variables for result and errors for be filled with the response of _unitOfWork
            BaseResponse<Role> result = new BaseResponse<Role>();
            List<string> err = new List<string>();
            List<DetailResponse> detailResponse = new  List<DetailResponse>();

            try
            {
                //Set in Data Response of result object   
                result.DataResponse = await _unitOfWork.RoleRepository.CreateRole(RoleToBeCreatedModel);

                //Set Successful in true because the commit was completed     
                result.Successful = true;

                //Set in Details local variable  object a message for successful execution in the Create
                detailResponse.Add(result.AddDetailResponse (RoleToBeCreatedModel.Id, "Regitro exitoso"));

                //Set Details from local variable to result before return
                result.Details = detailResponse;

            }
            catch (System.Exception ex )
            {
                //If exist a Exception it's catched and Set in err List the Message 
                err.Add("Error en RoleService -> CreateRole " + ex.Message);
                //Set in the result errors object the exception message
                result.errors = err;
            }          

            return result;
        } 
        /* /// <summary>
        /// Function thah Update a register for Role  
        /// </summary>
        /// <param name="RoleToBeUpdateModel">Instance of Role for be Updated</param>
        /// <param name="RoleForUpdateModel">Instance of Role with the Data to Update</param>
        /// <returns></returns>
        public async Task<BaseResponse<Role>>  UpdateRole(Role RoleToBeUpdateModel , Role RoleForUpdateModel)
        {

            //Declare variables for result and errors for be filled with the response of _unitOfWork
            BaseResponse<Role> result = new BaseResponse<Role>();
            List<string> err = new List<string>(); 
            List<DetailResponse> detailResponse = new  List<DetailResponse>();

            try
            {
                //Set in the object TO BE UPDATED (ORIGIN) the changues from object FOR UPDATE                
                RoleToBeUpdateModel.Name = RoleForUpdateModel.Name;
                RoleToBeUpdateModel.Description = RoleForUpdateModel.Description;
                RoleToBeUpdateModel.Status = RoleForUpdateModel.Status;
            

                //Call commit of the changues in the past step            
                await _unitOfWork.CommitAsync();  

                //Set Successful in true because the commit was completed     
                result.Successful = true;

                //Set in Data Response of result object   
                result.DataResponse = await _unitOfWork.RoleRepository.GetByIdAsync(RoleToBeUpdateModel.Id);

                //Set in Details local variable  object a message for successful execution in the Update
                detailResponse.Add(result.AddDetailResponse (RoleToBeUpdateModel.Id, "Actualización realizada correctamente"));

                //Set Details from local variable to result before return
                result.Details = detailResponse;
                
            }
            catch (System.Exception ex )
            {
                //If exist a Error un the transaction then set Error Detail for return in the result
                err.Add("Error en RoleService -> UpdateRole " + ex.Message);
                result.errors = err;                
            }

            //return result FROM SERVICE object
            return result;  
            
        } */
        /* /// <summary>
        /// Get the first or Default Role Register filter by Name 
        /// </summary>
        /// <param name="id">Id of Role for the search</param>
        /// <returns>BaseResponse<Role> with the result of the search By Name</returns>
        public async Task<BaseResponse<Role>>  GetRoleByName(string name)
        {
            //Declare variables for result and errors for be filled with the response of _unitOfWork
            BaseResponse<Role> result = new BaseResponse<Role>();
            List<string> err = new List<string>();

            try
            {
                //Using _unitOfWork call to a RoleRepository and through of the 
                //GetByIdAsync GENERIC method Search the first or Default
                result.DataResponse = await _unitOfWork.RoleRepository.GetRoleByName(name);

                //If the Query was Successful then in the result this flat in true
                result.Successful = true; 
            }
            catch (System.Exception ex )
            {
                //If exist a Exception it's catched and Set in err List the Message 
                err.Add("Error en RoleService -> GetRole " + ex.Message);
                //Set in the result errors object the exception message
                result.errors = err;
            }          

            return result;
            
        }*/
    } 
}
