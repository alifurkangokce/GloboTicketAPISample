﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Domain.Entities;
using MediatR;

namespace GloboTicket.TicketManagement.Application.Features.Events
{
    public class GetEventDetailQueryHandler:IRequestHandler<GetEventDetailQuery,EventDetailViewModel>
    {

        private readonly IAsyncRepository<Event> _eventRepository;
        private readonly IAsyncRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public GetEventDetailQueryHandler(IMapper mapper, IAsyncRepository<Event> eventRepository, IAsyncRepository<Category> categoryRepository)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<EventDetailViewModel> Handle(GetEventDetailQuery request, CancellationToken cancellationToken)
        {
            var @event = await _eventRepository.GetByIdAsync(request.Id);
            var eventDetailDto = _mapper.Map<EventDetailViewModel>(@event);
            var category = await _categoryRepository.GetByIdAsync(@event.CategoryId);
            eventDetailDto.Category = _mapper.Map<CategoryDto>(category);
            return eventDetailDto;
        }
    }
}
