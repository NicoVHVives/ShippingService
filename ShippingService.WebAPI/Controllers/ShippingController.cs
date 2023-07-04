using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShippingService.RepositoryLayer.Data;
using ShippingService.DomainLayer.Models;
using ShippingService.ServiceLayer.Services;
using ShippingService.ServiceLayer.DTO;

namespace ShippingService.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingController : ControllerBase
    {

        private readonly ShippingHistoryService _shippingHistoryService;
        private readonly WebShopUserService _webShopUserService;
        private readonly IConfiguration _config;
        private readonly MailService _mailService;


        public ShippingController(ShippingHistoryService shippingHistoryService, WebShopUserService webShopUserService, IConfiguration config, MailService mailService)
        {
            _shippingHistoryService = shippingHistoryService;
            _webShopUserService = webShopUserService;
            _config = config;
            _mailService = mailService;
        }

        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("GetShippingCost")]
        [Produces("application/json")]
        public ActionResult<ShippingResponseDTO> GetShippingCost([FromBody] ShippingRequestDTO request)
        {
            try
            {
                

                ShippingResponseDTO shipResp = new ShippingResponseDTO();

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                WebShopUser loggedInUser = getLoggedInUser(HttpContext);

                if(loggedInUser == null)
                    return Forbid();

                List<ShippingHistory> shippingHistory = _shippingHistoryService.GetAll(x => x.WebshopID == loggedInUser.Id).ToList();

                ShippingHistory entity = shippingHistory.FirstOrDefault(x => x.TransactionGuid == request.TransactionGuid);

                //Check if the transaction already exists in the db
                if (entity == null)
                {
                    DateTime created = DateTime.UtcNow;

                    entity = new ShippingHistory
                    {
                        WebshopID = loggedInUser.Id,
                        TransactionGuid = request.TransactionGuid,
                        ClientGuid = request.ClientGuid,
                        ItemGuid = request.ItemGuid,
                        CreatedDate = created,
                        UpdatedDate = created
                    };

                    _shippingHistoryService.Insert(entity);

                    //Check if the client has reached x shipments over the past x days

                    shipResp.Account = entity.ClientGuid;
                    shipResp.Product = entity.ItemGuid;

                    int nrOfShipments = _shippingHistoryService.GetNrOfShipments(entity.ClientGuid, entity.CreatedDate);

                    if (nrOfShipments <= _config.GetValue<int>("ShippingParameters:MaxFreeShipments"))
                    {
                        shipResp.Access = ShippingResponseEnum.FreePortage.ToString();


                        if (nrOfShipments == 1)
                        {
                            //_mailService.SendEmail(_mailService.generateFirstShipmentMail(entity.ClientGuid));
                        }
                        else if (nrOfShipments == _config.GetValue<int>("ShippingParameters:MaxFreeShipments"))
                        {
                            //_mailService.SendEmail(_mailService.generateLastShipmentMail(entity.ClientGuid));
                        }
                    }
                    else
                    {
                        shipResp.Access = ShippingResponseEnum.PortageAdded.ToString();
                    }

                    return Ok(shipResp);
                }
                else
                {
                    shipResp.Account = entity.ClientGuid;
                    shipResp.Product = entity.ItemGuid;
                    shipResp.Access = ShippingResponseEnum.AlreadySent.ToString();

                    return Ok(shipResp);
                }

            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("GetShipmentsForClient")]
        [Produces("application/json")]
        public ActionResult<IEnumerable<ShippingHistoryDTO>> GetShipmentsForClient(Guid clientGuid)
        {

            try
            {

                WebShopUser loggedInUser = getLoggedInUser(HttpContext);

                if (loggedInUser == null)
                    return Forbid();

                IEnumerable<ShippingHistory> results = _shippingHistoryService.GetAll(x => x.WebshopID == loggedInUser.Id && x.ClientGuid == clientGuid);

                if(results.Count() == 0)
                {
                    return NoContent();
                }
                else
                {
                    return Ok(results);
                }
            }
            catch(Exception ex)
            {
               
                return StatusCode(500,ex.Message);
            }


        }

        private WebShopUser getLoggedInUser(HttpContext context)
        {
            System.Security.Claims.ClaimsPrincipal user = context.User;

            if (user == null || user.Identity == null)
            {
                return null;
            }

            return _webShopUserService.Get(x => x.UserName == user.Identity.Name);
        }
    }
}
