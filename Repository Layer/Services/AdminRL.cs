using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Common_Layer;
using Common_Layer.Model;
using Microsoft.Extensions.Configuration;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.Services
{
    public class AdminRL : IAdminRL
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IConfiguration _configuration;
        public AdminRL(ApplicationDbContext applicationDbContext, IConfiguration configuration)
        {
            _applicationDbContext = applicationDbContext;
            _configuration = configuration;
        }

        public async Task<AddProductResponse> AddProduct(AddProductRequest request)
        {
            AddProductResponse response = new AddProductResponse();
            response.IsSuccess = true;
            response.Message = "Add Product Successfully";

            try
            {

                Account account = new Account(
                                _configuration["CloudinarySettings:CloudName"],
                                _configuration["CloudinarySettings:ApiKey"],
                                _configuration["CloudinarySettings:ApiSecret"]);

                var path = request.File.OpenReadStream();

                Cloudinary cloudinary = new Cloudinary(account);

                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(request.File.FileName, path),
                    //Folder=""
                };
                var uploadResult = await cloudinary.UploadAsync(uploadParams);

                Product product = new Product()
                {
                    InsertionDate = DateTime.Now,
                    ProductName = request.ProductName,
                    ProductDescription = request.ProductDescription,
                    ProductPrice = request.ProductPrice,
                    ProductType = request.ProductType,
                    ProductImageUrl = uploadResult.Url.ToString(),
                    PublicID = uploadResult.PublicId.ToString(),
                    IsActive = true
                };

                await _applicationDbContext.AddAsync(product);
                int Result = await _applicationDbContext.SaveChangesAsync();
                if (Result <= 0)
                {
                    response.IsSuccess = false;
                    response.Message = "Something Went Wrong At AddProduct";
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

        public async Task<DeleteProductResponse> DeleteProduct(DeleteProductRequest request)
        {
            DeleteProductResponse response = new DeleteProductResponse();
            response.IsSuccess = true;
            response.Message = "Delete Product Successfully";

            try
            {
                var Result = await _applicationDbContext.Products.FindAsync(request.ProductID);
                if (Result == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Product Not Found";
                }

                _applicationDbContext.Products.Remove(Result);
                int DeleteResult = await _applicationDbContext.SaveChangesAsync();
                if (DeleteResult <= 0)
                {
                    response.IsSuccess = false;
                    response.Message = "Something went Wrong";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Message : " + ex.Message;
            }

            return response;
        }

        public async Task<GetAllProductResponse> GetAllProduct(GetAllProductRequest request)
        {
            GetAllProductResponse response = new GetAllProductResponse();
            response.IsSuccess = true;
            response.Message = "Fetch Product Successfully";

            try
            {
                response.data = new List<Product>();
                response.data = request.Type.ToLower() == "all" ?
                    // Fetch All Product
                    _applicationDbContext.Products
                    .Skip((request.PageNumber - 1) * request.NumberOfRecordPerPage)
                    .Take(request.NumberOfRecordPerPage)
                    .OrderByDescending(X => X.InsertionDate)
                    .ToList()
                    :
                    //Fetch Plan And Device Product
                    _applicationDbContext.Products
                    .Where(X => X.ProductType.ToLower() == request.Type.ToLower())
                    .Skip((request.PageNumber - 1) * request.NumberOfRecordPerPage)
                    .Take(request.NumberOfRecordPerPage)
                    .OrderByDescending(X => X.InsertionDate)
                    .ToList();

                if (response.data.Count == 0)
                {
                    response.Message = "Data Not Found";
                    return response;
                }

                response.CurrentPage = request.PageNumber;
                if (request.Type.ToLower() == "all")
                {
                    response.TotalRecords = _applicationDbContext.Products.Count();
                }
                else
                {
                    response.TotalRecords = _applicationDbContext.Products
                        .Where(X => X.ProductType.ToLower() == request.Type.ToLower()).Count();
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

        public async Task<UpdateProductResponse> UpdateProduct(UpdateProductRequest request)
        {
            UpdateProductResponse response = new UpdateProductResponse();
            response.IsSuccess = true;
            response.Message = "Add Product Successfully";

            try
            {
                //Search ProductID Exist or not
                var Product = await _applicationDbContext.Products.FindAsync(request.ProductID);

                if (Product == null)
                {
                    response.IsSuccess = false;
                    response.Message = "ProductID not Found";
                    return response;
                }

                //New Image
                if (request.File != null)
                {
                    //
                    Account account = new Account(
                                    _configuration["CloudinarySettings:CloudName"],
                                    _configuration["CloudinarySettings:ApiKey"],
                                    _configuration["CloudinarySettings:ApiSecret"]);

                    Cloudinary cloudinary = new Cloudinary(account);

                    // Delete Old Image
                    var deletionParams = new DeletionParams(request.PublicID)
                    {
                        ResourceType = ResourceType.Image
                    };

                    var deletionResult = cloudinary.Destroy(deletionParams);
                    var Result = deletionResult.Result.ToString();
                    if (Result.ToLower() != "ok")
                    {
                        response.IsSuccess = false;
                        response.Message = "Something Went To Wrong In Cloudinary Destroy Method";
                    }

                    //Upload New Image
                    var path = request.File.OpenReadStream();
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(request.File.FileName, path),
                    };

                    var uploadResult = await cloudinary.UploadAsync(uploadParams);
                    Product.ProductImageUrl = uploadResult.Url.ToString();
                    Product.PublicID = uploadResult.PublicId;

                }
                else
                {
                    // Image Not Update (Pass Old Url And Public ID)
                    Product.ProductImageUrl = request.ImageUrl;
                    Product.PublicID = request.PublicID;
                }

                Product.ProductDescription = request.ProductDescription;
                Product.ProductName = request.ProductName;
                Product.ProductPrice = request.ProductPrice;
                Product.ProductType = request.ProductType;

                int Result1 = await _applicationDbContext.SaveChangesAsync();
                if (Result1 <= 0)
                {
                    response.IsSuccess = false;
                    response.Message = "Something Went Wrong";
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
