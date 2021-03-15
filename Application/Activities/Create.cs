﻿using Application.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistance;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Activities
{
    public class Create
    {

        public class Command : IRequest
        {
            public Guid Id { get; set; }
 
            public string Title { get; set; }
            
            public DateTime Date { get; set; }
            public string Description { get; set; }
            public string Category { get; set; }
            public string City { get; set; }
            public string Venue { get; set; }

        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Title).NotEmpty();
                RuleFor(x => x.Date).NotEmpty();
                RuleFor(x => x.Description).NotEmpty();
                RuleFor(x => x.Category).NotEmpty();
                RuleFor(x => x.City).NotEmpty();
                RuleFor(x => x.Venue).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context ,IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _context = context;

            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {

                var activity = new Activity
                {
                    Id = request.Id,
                    Title = request.Title,
                    Description = request.Description,
                    Date = request.Date,
                    Category = request.Category,
                    City = request.City,
                    Venue = request.Venue
                };
                _context.Activities.Add(activity);

                var user = await _context.Users.SingleOrDefaultAsync(x =>x.UserName == _userAccessor.getCurrentUsername());

                var attendee = new UserActivity
                {
                    AppUser = user,
                    Activity = activity,
                    IsHost = true,
                    DateJoined = DateTime.Now
                };

                _context.UserActivities.Add(attendee);
                var success = await _context.SaveChangesAsync() > 0 ;
                
                if (success) return Unit.Value;
                else                {
                    throw new Exception("Problem Saving Activity");
                }
            }
        }
    }
}
