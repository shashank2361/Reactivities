using Application.Errors;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistance;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Activities
{
    public class Details
    {
        public class Query : IRequest<ActivityDto> {
            public Guid Id  { get; set; }
        }


        public class Handler : IRequestHandler<Query, ActivityDto>
        {

            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler( DataContext context , IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<ActivityDto> Handle(Query request, CancellationToken cancellationToken)
            {
                //Eager Loading
                // var activity = await _context.Activities
                //     .Include( x => x.UserActivities)
                //     .ThenInclude( x=> x.AppUser)
                //     .SingleOrDefaultAsync(x => x.Id == request.Id); 

                var activity = await _context.Activities.FindAsync(request.Id);
                    //.Include( x => x.UserActivities)
                    //.ThenInclude( x=> x.AppUser)
                    //.SingleOrDefaultAsync(x => x.Id == request.Id);

                if (activity == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { activity = "Could not find activity" });
                }
                var activityToReturn = _mapper.Map<Activity, ActivityDto>(activity);

                return activityToReturn;
            }
        }
    }
}
