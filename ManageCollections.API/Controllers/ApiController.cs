﻿using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace ManageCollections.API.Controllers
{
    public abstract class ApiController<TModel> : ControllerBase
    {
        protected IMapper _mapper => HttpContext.RequestServices.GetRequiredService<IMapper>();

        protected IValidator<TModel> _validator => HttpContext.RequestServices.GetRequiredService<IValidator<TModel>>();
    }
}
