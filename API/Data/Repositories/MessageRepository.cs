using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public MessageRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public void AddMessage(Message message)
        {
            _context.PrivateMessages.Add(message);
        }

        public void DeleteMessage(Message message)
        {
            _context.PrivateMessages.Remove(message);
        }

        public async Task<Message> GetMessageById(int msgId)
        {
            return await _context.PrivateMessages
                .Include(u => u.SenderAppUser)
                .Include(u => u.RecipientAppUser)
                .SingleOrDefaultAsync(x => x.Id == msgId);
                //.FindAsync(msgId);
        }

        public async Task<PagedList<MessageDto>> GetMessagesForUser(MessageParams messageParams)
        {
            var query = _context.PrivateMessages
                .OrderByDescending(m => m.TimePosted)
                .AsQueryable();

            query = messageParams.Container switch
            {
                "Inbox" => query.Where(u => u.RecipientAppUser.UserName == messageParams.Username 
                    && u.RecipientDeleted == false),
                "Outbox" => query.Where(u => u.SenderAppUser.UserName == messageParams.Username
                    && u.SenderDeleted == false),
                _ => query.Where(u => u.RecipientAppUser.UserName ==
                    messageParams.Username && u.RecipientDeleted == false && u.DateRead == null)
            };

            var messages = query.ProjectTo<MessageDto>(_mapper.ConfigurationProvider);

            return await PagedList<MessageDto>.CreateAsync(messages, messageParams.PageNumber, messageParams.PageSize);

        }

        public async Task<IEnumerable<MessageDto>> GetMessageThread(string currentUsername, 
            string recipientUsername)
        {
            var messages = await _context.PrivateMessages
                .Include(u => u.SenderAppUser)
                .Include(u => u.RecipientAppUser)
                .Where(m => m.RecipientAppUser.UserName == currentUsername && m.RecipientDeleted == false
                        && m.SenderAppUser.UserName == recipientUsername
                        || m.RecipientAppUser.UserName == recipientUsername
                        && m.SenderAppUser.UserName == currentUsername && m.SenderDeleted == false
                )
                .OrderBy(m => m.TimePosted)
                .ToListAsync();

            var unreadMessages = messages.Where(m => m.DateRead == null 
                && m.RecipientAppUser.UserName == currentUsername).ToList();

            if (unreadMessages.Any())
            {
                foreach (var message in unreadMessages)
                {
                    message.DateRead = DateTime.UtcNow;
                }

                await _context.SaveChangesAsync();
            }

            return _mapper.Map<IEnumerable<MessageDto>>(messages);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        
        public void AddGroup(Group group)
        {
            _context.Groups.Add(group);
        }

        public async Task<Connection> GetConnection(string connectionId)
        {
            return await _context.Connections.FindAsync(connectionId);
        }

        public async Task<Group> GetGroupForConnection(string connectionId)
        {
            return await _context.Groups
                .Include(c => c.Connections)
                .Where(c => c.Connections.Any(x => x.ConnectionId == connectionId))
                .FirstOrDefaultAsync();
        }

        public async Task<Message> GetMessage(int id)
        {
            return await _context.PrivateMessages
                .Include(u => u.SenderAppUser)
                .Include(u => u.RecipientAppUser)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Group> GetMessageGroup(string groupName)
        {
            return await _context.Groups
                .Include(x => x.Connections)
                .FirstOrDefaultAsync(x => x.Name == groupName);
        }

        public void RemoveConnection(Connection connection)
        {
            _context.Connections.Remove(connection);
        }

    }
}