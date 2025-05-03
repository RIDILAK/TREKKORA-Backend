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
        Task<Responses<NotificationDto>> SendNotification(Guid senderId, Guid ReceiverId, string message);
        Task<Responses<List<NotificationResponseDto>>> GetNotifications(Guid receiverId);
        Task<Responses<NotificationResponseDto>> MarkAsRead(Guid id);
        Task<Responses<string>> DeleteNotification(Guid id);
    }
    public class NotificationServices : INotificationServices
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;
        private readonly IHubContext<ChatHub> _context;

        public NotificationServices(INotificationRepository notificationRepository, IMapper mapper, IHubContext<ChatHub> hubContext)
        {

            _notificationRepository = notificationRepository;
            _mapper = mapper;
            hubContext = _context;
        }

        public async Task<Responses<NotificationDto>> SendNotification(Guid senderId, Guid ReceiverId, string message)
        {
            var entity = new Notification
            {
                Id = Guid.NewGuid(),
                senderId = senderId,
                ReceiverId = ReceiverId,
                Message = message
            };
            var saved = await _notificationRepository.AddNotification(entity);
            var dto = _mapper.Map<NotificationDto>(saved);

            //await _context.Clients.User(dto.ReceiverId.ToString())
            // .SendAsync("recieveMessage", dto.SenderId, dto.Message);

            return new Responses<NotificationDto> { Data = dto, Message = "Notifcation added succesfully", StatuseCode = 200 };
        }

        public async Task<Responses<List<NotificationResponseDto>>> GetNotifications(Guid receiverId)
        {
            var data = await _notificationRepository.GetByReceiverId(receiverId);
            var mapper = _mapper.Map<List<NotificationResponseDto>>(data);
            return new Responses<List<NotificationResponseDto>> { Data = mapper, StatuseCode = 200, Message = "Fetched" };

        }

        public async Task<Responses<NotificationResponseDto>> MarkAsRead(Guid id)
        {
            var result = await _notificationRepository.MarkAsRead(id);
            if (result == null)
            {
                return new Responses<NotificationResponseDto> { Message = "id not found", StatuseCode = 200 };

            }
            var mapped = _mapper.Map<NotificationResponseDto>(result);
            return new Responses<NotificationResponseDto> { Data = mapped, StatuseCode = 200, Message = "Readed" };
        }

        public async Task<Responses<string>> DeleteNotification(Guid id)
        {
            var isDeleted = await _notificationRepository.DeleteNotification(id);
            if (isDeleted == null)
            {
                return new Responses<string> { Message = "id not found", StatuseCode = 401 };

            }
            return new Responses<string> { Message = "Notification deleted succesfully", StatuseCode = 200 };
        }
    }
}
