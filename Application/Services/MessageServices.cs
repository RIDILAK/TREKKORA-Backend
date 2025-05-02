using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Application.Services
{
    public interface IMessageServices
    {
        Task<Responses<MessageResponseDto>> AddMessageAsync(MessageCreateDto dto);
        Task<Responses<IEnumerable<MessageResponseDto>>> GetMessagesAsync(Guid senderId,Guid receiverId);

        Task<Responses<MessageResponseDto>> MarkAsRead(Guid id);

    }
    public class MessageServices:IMessageServices
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;

        public MessageServices(IMessageRepository messageRepository, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
        }

        public async Task<Responses<MessageResponseDto>> AddMessageAsync(MessageCreateDto dto)
        {
            var mapped = _mapper.Map<Message>(dto);
            var saved = await _messageRepository.AddMessage(mapped);
            var responseDto = _mapper.Map<MessageResponseDto>(saved);

            return new Responses<MessageResponseDto>
            {
                Message = "Message created successfully",
                Data = responseDto,
                StatuseCode = 200
            };
        }
       public async Task<Responses<IEnumerable<MessageResponseDto>>> GetMessagesAsync(Guid senderId, Guid receiverId)
        {
            var message= await _messageRepository.GetMessage(senderId, receiverId);
            var mapped= _mapper.Map<IEnumerable<MessageResponseDto>>(message);
            return new Responses<IEnumerable<MessageResponseDto>> { Data = mapped,StatuseCode=200,Message="Message Retrived" };
        }

        public async Task<Responses<MessageResponseDto>> MarkAsRead(Guid id)
        {
            var message = await _messageRepository.MarkAsRead(id);
            if (message == null)
            {
                return new Responses<MessageResponseDto>
                {
                    StatuseCode = 404,
                    Message = "Message not found"
                };
            }

            var mapped = _mapper.Map<MessageResponseDto>(message);
            return new Responses<MessageResponseDto>
            {
                Data = mapped,
                StatuseCode = 200,
                Message = "Marked as read"
            };
        }


    }
}

