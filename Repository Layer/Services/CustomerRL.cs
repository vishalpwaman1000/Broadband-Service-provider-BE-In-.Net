using Common_Layer;
using Common_Layer.Model;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Repository_Layer.Services
{
    public class CustomerRL : ICustomerRL
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public CustomerRL(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<CreateTicketResponse> CreateTicket(CreateTicketRequest request)
        {
            CreateTicketResponse response = new CreateTicketResponse();
            response.IsSuccess = true;
            response.Message = "Raise Ticket Successfully";

            try
            {
                //InsertionDate, UserID, Reportor, Assigner, PlanType, RaiseType, Summary, Description, Status
                Ticket ticket = new Ticket()
                {
                    InsertionDate = DateTime.Now,
                    UserID = request.UserID,
                    Reportor = request.Reportor,
                    Assigner = "admin",
                    PlanType = request.PlanType,
                    RaiseType = request.RaiseType,
                    Summary = request.Summary,
                    Description = request.Description,
                    Status = "pending"
                };

                await _applicationDbContext.AddAsync(ticket);
                int Result = await _applicationDbContext.SaveChangesAsync();
                if (Result <= 0)
                {
                    response.IsSuccess = false;
                    response.Message = "Something Went Wrong At Create Ticket";
                    return response;
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Message : " + ex.Message;
            }

            return response;
        }

        public async Task<GetCustomerAddressResponse> GetCustomerAddress(int UserID)
        {
            GetCustomerAddressResponse response = new GetCustomerAddressResponse();
            response.IsSuccess = true;
            response.Message = "Fetch Data Successful";

            try
            {

                var GetCustomerAddress = _applicationDbContext.UserPersonalDetails
                    .Where(X => X.UserID == UserID)
                    .FirstOrDefault();

                if (GetCustomerAddress == null)
                {
                    response.IsSuccess = false;
                    response.Message = "User Data Not Found";
                    return response;
                }

                response.data = new UserPersonalDetail();
                response.data = GetCustomerAddress;

                /*
                response.data = new UserPersonalDetail();
                response.data.Address = GetCustomerAddress.Address;
                response.data.Address = GetCustomerAddress.Address;
                response.data.City = GetCustomerAddress.City;
                response.data.CompanyName = GetCustomerAddress.CompanyName;
                response.data.DOB = GetCustomerAddress.DOB;
                response.data.Email = GetCustomerAddress.Email;
                response.data.FirstName = GetCustomerAddress.FirstName;
                response.data.Gender = GetCustomerAddress.Gender;
                response.data.HomePhone = GetCustomerAddress.HomePhone;
                response.data.LastName = GetCustomerAddress.LastName;
                response.data.Occupation = GetCustomerAddress.Occupation;
                response.data.PersonalDetailID = GetCustomerAddress.PersonalDetailID;
                response.data.PersonalNumber = GetCustomerAddress.PersonalNumber;
                response.data.Position = GetCustomerAddress.Position;
                response.data.State = GetCustomerAddress.State;
                response.data.UserID = GetCustomerAddress.UserID;
                response.data.ZipCode = GetCustomerAddress.ZipCode;
                */

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Message : " + ex.Message;
            }

            return response;
        }

        public async Task<GetPurchaseProductResponse> GetPurchaseProduct(GetPurchaseProductRequest request)
        {
            GetPurchaseProductResponse response = new GetPurchaseProductResponse();
            response.IsSuccess = true;
            response.Message = "Purchase Product Fetch Successful";

            try
            {
                //Inner Join
                response.data = new List<PurchaseProductDetail>();
                if (request.ProductType.ToLower() == "all")
                {
                    //all
                    var Result = (from c in _applicationDbContext.Products
                                  join P in _applicationDbContext.PurchaseProducts
                                  on c.ProductID equals P.ProductID
                                  where P.UserID == request.UserID
                                  select new
                                  {
                                      PurchaseId = P.PurchaseId,
                                      ProductID = c.ProductID,
                                      ProductDescription = c.ProductDescription,
                                      ProductImageUrl = c.ProductImageUrl,
                                      ProductName = c.ProductName,
                                      ProductPrice = c.ProductPrice,
                                      ProductType = c.ProductType,
                                      PublicID = c.PublicID,
                                      InsertionDate = P.InsertionDate,
                                      IsActive = c.IsActive
                                  })
                  .OrderByDescending(X => X.PurchaseId)
                  .Skip(request.NumberOfRecordPerPage * (request.PageNumber - 1))
                  .Take(request.NumberOfRecordPerPage)
                  .ToList();

                    foreach (var data in Result)
                    {
                        response.data.Add(
                            new PurchaseProductDetail()
                            {
                                PurchaseId = data.PurchaseId,
                                InsertionDate = data.InsertionDate,
                                UserID = request.UserID,
                                ProductID = data.ProductID,
                                ProductType = data.ProductType,
                                ProductName = data.ProductName,
                                ProductDescription = data.ProductDescription,
                                ProductPrice = data.ProductPrice,
                                ProductImageUrl = data.ProductImageUrl,
                                PublicID = data.PublicID,
                                IsActive = data.IsActive
                            });
                    }
                }
                else
                {
                    //device & plan
                    var Result = (from c in _applicationDbContext.Products
                                  join P in _applicationDbContext.PurchaseProducts
                                  on c.ProductID equals P.ProductID
                                  where P.UserID == request.UserID && P.ProductType == request.ProductType
                                  select new
                                  {
                                      PurchaseId = P.PurchaseId,
                                      ProductID = c.ProductID,
                                      ProductDescription = c.ProductDescription,
                                      ProductImageUrl = c.ProductImageUrl,
                                      ProductName = c.ProductName,
                                      ProductPrice = c.ProductPrice,
                                      ProductType = c.ProductType,
                                      PublicID = c.PublicID,
                                      InsertionDate = P.InsertionDate,
                                      IsActive = c.IsActive
                                  })
                  .OrderByDescending(X => X.PurchaseId)
                  .Skip(request.NumberOfRecordPerPage * (request.PageNumber - 1))
                  .Take(request.NumberOfRecordPerPage)
                  .ToList();

                    foreach (var data in Result)
                    {
                        response.data.Add(
                            new PurchaseProductDetail()
                            {
                                PurchaseId = data.PurchaseId,
                                InsertionDate = data.InsertionDate,
                                UserID = request.UserID,
                                ProductID = data.ProductID,
                                ProductType = data.ProductType,
                                ProductName = data.ProductName,
                                ProductDescription = data.ProductDescription,
                                ProductPrice = data.ProductPrice,
                                ProductImageUrl = data.ProductImageUrl,
                                PublicID = data.PublicID,
                                IsActive = data.IsActive
                            });
                    }
                }



                if (response.data.Count == 0)
                {
                    response.Message = "Data Not Found";
                    return response;
                }

                response.CurrentPage = request.PageNumber;
                if (request.ProductType.ToLower() == "all")
                {
                    response.TotalRecords = _applicationDbContext.PurchaseProducts.Count();
                }
                else
                {
                    response.TotalRecords = _applicationDbContext.PurchaseProducts
                        .Where(X => X.ProductType.ToLower() == request.ProductType.ToLower() &&
                                              X.UserID == request.UserID).Count();
                }
                response.TotalPage = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(response.TotalRecords / request.NumberOfRecordPerPage)));
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Message : " + ex.Message;
            }

            return response;
        }

        public async Task<GetTicketHistoryResponse> GetTicketHistory(GetTicketHistoryRequest request)
        {
            GetTicketHistoryResponse response = new GetTicketHistoryResponse();
            response.IsSuccess = true;
            response.Message = "Fetch All Ticket History";

            try
            {

                response.data = _applicationDbContext.Tickets
                    .Where(X => X.UserID == request.UserID)
                    .Skip((request.PageNumber - 1) * request.NumberOfRecordPerPage)
                    .Take(request.NumberOfRecordPerPage)
                    .ToList();

                response.data = response.data.Count == 0 ? null : response.data;

                response.CurrentPage = request.PageNumber;
                response.TotalRecords = _applicationDbContext.Tickets.Count();
                response.TotalPage = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(response.TotalRecords / request.NumberOfRecordPerPage)));


            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Message : " + ex.Message;
            }

            return response;
        }

        public async Task<PurchaseProductResponse> PurchaseProduct(PurchaseProductRequest request)
        {
            PurchaseProductResponse response = new PurchaseProductResponse();
            response.IsSuccess = true;
            response.Message = "Purchase Product Successfully";

            try
            {

                PurchaseProduct purchase = new PurchaseProduct()
                {
                    UserID = request.UserID,
                    ProductID = request.ProductID,
                    ProductType = request.ProductType,
                    InsertionDate = DateTime.Now
                };

                await _applicationDbContext.AddAsync(purchase);
                int Result = await _applicationDbContext.SaveChangesAsync();
                if (Result <= 0)
                {
                    response.IsSuccess = false;
                    response.Message = "Something Went Wrong At Purchase Product";
                    return response;
                }
                Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Message : " + ex.Message;
            }

            return response;
        }

        public async Task<UpdateCustomerAddressResponse> UpdateCustomerAddress(UpdateCustomerAddressRequest request, string Role)
        {
            UpdateCustomerAddressResponse response = new UpdateCustomerAddressResponse();
            response.IsSuccess = true;
            response.Message = "Update Customer Detail Successfully";

            try
            {

                var UserPersonalDetail = _applicationDbContext.UserPersonalDetails.Where(X => X.UserID == request.UserID).FirstOrDefault();
                if (UserPersonalDetail == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Something Went Wrong";
                    return response;
                }

                //UserID FirstName LastName Address City State ZipCode HomePhone 
                //PersonalNumber Email Gender DOB Occupation CompanyName Position
                UserPersonalDetail.Address = request.Address;
                UserPersonalDetail.City = request.City;
                UserPersonalDetail.State = request.State;
                UserPersonalDetail.ZipCode = request.ZipCode;
                UserPersonalDetail.HomePhone = request.HomePhone;
                UserPersonalDetail.PersonalNumber = request.PersonalNumber;
                UserPersonalDetail.DOB = request.DOB;
                UserPersonalDetail.Email = request.Email;
                UserPersonalDetail.Gender = request.Gender;

                if (Role != "customer")
                {
                    UserPersonalDetail.Position = request.Position;
                }
                else
                {
                    UserPersonalDetail.Occupation = request.Occupation;
                    UserPersonalDetail.CompanyName = request.CompanyName;
                }

                int Result = await _applicationDbContext.SaveChangesAsync();
                if (Result <= 0)
                {
                    response.IsSuccess = false;
                    response.Message = "Update Customer Detail Failed";
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Message : " + ex.Message;
            }

            return response;
        }
    }
}
