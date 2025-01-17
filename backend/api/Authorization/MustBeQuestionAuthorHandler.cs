﻿using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using api.Data;

namespace api.Authorization
{
    public class MustBeQuestionAuthorHandler :

       AuthorizationHandler<MustBeQuestionAuthorRequirement>
    {

        private readonly IDataRepository _dataRepository;

        private readonly IHttpContextAccessor

         _httpContextAccessor;

        public MustBeQuestionAuthorHandler(

          IDataRepository dataRepository,

          IHttpContextAccessor httpContextAccessor) {

            _dataRepository = dataRepository;

            _httpContextAccessor = httpContextAccessor;

        }

        protected async override Task
          HandleRequirementAsync(
            AuthorizationHandlerContext context,
            MustBeQuestionAuthorRequirement requirement) {
            // TODO - check that the user is authenticated

            if (!context.User.Identity.IsAuthenticated) {
                context.Fail();
                return;
            }

            // TODO - get the question id from the request
            var questionId =
                _httpContextAccessor.HttpContext.Request
                    .RouteValues["questionId"];

            int questionIdAsInt = Convert.ToInt32(questionId);

            // TODO - get the user id from the name
            // identifier claim

            var userID = context.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            // TODO - get the question from the data repository

            var question =
                await _dataRepository.GetQuestion(questionIdAsInt);

            // TODO - if the question can't be found go to
            // the next piece of middleware

            if (question != null) {
                // let it through so the controller can return a 404
                context.Succeed(requirement);

                // TODO - return failure if the user id in the
                // question from the data repository is
                // different to the user id in the request

                if (question.UserId != userID) {
                    context.Fail();
                    return;
                }
                context.Succeed(requirement);

                // TODO - return success if we manage to get
                // here
            }
        }
    }
}
