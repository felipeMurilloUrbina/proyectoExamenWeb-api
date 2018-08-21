using AutoMapper;
using Proyecto.Examen.WebApi._Dtos;
using Proyecto.Examen.WebApi._Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Examen.WebApi._Formats
{
    public static  class AutoMapperConfig
    {
        public static void Config()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<User, UserDTO>().ReverseMap();
            });
        }
    }
}