using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.SignalR;
using TREKKORA_Backend.Hubs;

namespace Application.Services
{
    public interface INotificationServices
    {
        Task<Responses<NotoficationDto>> SendNotification(Guid senderId, Guid ReceiverId, string message);
    }
    public class NotificationServices:INotificationServices
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;
        private readonly IHubContext<ChatHub>_context;

        public NotificationServices(INotificationRepository notificationRepository, IMapper mapper, IHubContext<ChatHub> hubContext)
        {

            _notificationRepository = notificationRepository;
            _mapper = mapper;
            hubContext=_context;
        }

      public async  Task<Responses<NotoficationDto>> SendNotification(Guid senderId, Guid ReceiverId, string message)
        {
            var entity = new Notification
            {
                Id = Guid.NewGuid(),
                senderId = senderId,
                ReceiverId = ReceiverId,
                Message = message
            };
            var saved=await _notificationRepository.AddNotification(entity);
            var dto= _mapper.Map<NotoficationDto>(saved);

            await _context.Clients.User(dto.ReceiverId.ToString())
             .SendAsync("recieveMessage", dto.SenderId, dto.Message);

            return new Responses<NotoficationDto> { Data = dto, Message = "Notifcation added succesfully", StatuseCode = 200 };
        }
    }
}
