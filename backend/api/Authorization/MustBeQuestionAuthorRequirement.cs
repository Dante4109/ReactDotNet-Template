﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace api.Authorization
{
    public class MustBeQuestionAuthorRequirement:IAuthorizationRequirement
    {
        public MustBeQuestionAuthorRequirement()
        {
        }
    }
}
