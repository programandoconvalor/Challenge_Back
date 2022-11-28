using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tekchoice.Core.DTO;
using tekchoice.Data.Models;
using tekchoice.Operations.Interfaces;
using Microsoft.Extensions.Configuration;

namespace tekchoice.API.Controllers
{
    [Produces("application/json")]
    [Route("calcular/GrasaCorporal")]
    public class CalculoController : ControllerBase
    {
        private readonly ICalculo _calculoService;
        public IConfiguration _configuration { get; }
        public CalculoController
            (ICalculo calculoService, IConfiguration configuration)
        {
            _calculoService = calculoService;
            _configuration = configuration;
        }
        

        // [Authorize]
        [HttpPost]
        [Route("Data")]
        public async Task<ActionResult<IEnumerable<CalculoModel>>>
            Create([FromBody] CalculoModel val)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(400, new Response_DTO<FormatException>()
                    {
                        Success = false,
                        Message = _configuration
                                    .GetSection("Messages")
                                    .GetSection("Error").
                                     GetSection("ModelState").Value,
                        Data = new List<FormatException>()
                    });
                }
                CalculoModel data = _calculoService.Create(val);
                return Ok(new Response_DTO<CalculoModel>()
                {
                    Success = data == null ? false : true,
                    Message = data == null ? _configuration
                                                .GetSection("Messages")
                                                .GetSection("Error").
                                                 GetSection("Create").Value
                                            :
                                             _configuration
                                                .GetSection("Messages")
                                                .GetSection("Success").
                                                 GetSection("Create").Value,
                    Data = data
                });
            }
            catch (Exception exception)
            {
                return StatusCode(500, new Response_DTO<FormatException>()
                {
                    Success = false,
                    Message = exception.Message.ToString(),
                    Data = new List<FormatException>()
                });
            }
        }

        
    }
}

