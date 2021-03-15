using Application.Errors;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Profiles
{
    public class Details
        {
            public class Query : IRequest<Profile>
            {
                public string Username { get; set; }
            }


            public class Handler : IRequestHandler<Query, Profile>
            {

                private readonly DataContext _context;
              //  private readonly IMapper _mapper;

                public Handler(DataContext context/*, IMapper mapper*/)
                {
                    _context = context;
                    //_mapper = mapper;
                }
                public async Task<Profile> Handle(Query request, CancellationToken cancellationToken)
                {
                    
                    var user = await _context.Users.SingleOrDefaultAsync( x => x.UserName == request.Username );
                   

                    if (user == null)
                    {
                        throw new RestException(HttpStatusCode.NotFound, new { activity = "Could not find user" });
                    }
                   
                    return new Profile { 
                    DisplayName = user.DisplayName,
                    Username = user.UserName,
                    Image = user.Photos.FirstOrDefault(x => x.IsMain)?.Url,
                    Photos = user.Photos,
                    Bio = user.Bio
                    };
                }
            }
        }
    }


 

