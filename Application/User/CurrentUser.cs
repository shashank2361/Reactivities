using Application.Errors;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.User
{
    public class CurrentUser
    {


        public class Query : IRequest<User>
        {             
        }


        public class Handler : IRequestHandler<Query, User>
        {
            private readonly UserManager<AppUser> _userManager;
             private readonly IJwtGenerator _jwtGenerator;
             private readonly IUserAccessor _userAccessor;

            public Handler(UserManager<AppUser> userManager, IJwtGenerator jwtGenerator , IUserAccessor userAccessor)
            {
                this._jwtGenerator = jwtGenerator;
                this._userManager = userManager;
                this._userAccessor = userAccessor;
            }

            public async Task<User> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByNameAsync(_userAccessor.getCurrentUsername());
                if (user == null)
                    throw new RestException(HttpStatusCode.NotFound);

                 
                    // TODO generate token
                    return new User
                    {
                        DisplayName = user.DisplayName,
                        Token = _jwtGenerator.CreateToken(user),
                        Username = user.UserName,
                        Image = user.Photos.FirstOrDefault(x => x.IsMain)?.Url
                    };
                
                throw new RestException(HttpStatusCode.Unauthorized);
            }
        }
    }
}
