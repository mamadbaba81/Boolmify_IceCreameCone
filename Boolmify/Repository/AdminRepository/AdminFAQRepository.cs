    using Boolmify.Data;
    using Boolmify.Dtos.FAQ;
    using Boolmify.Interfaces.ADminRepository;
    using Boolmify.Models;
    using Microsoft.EntityFrameworkCore;

    namespace Boolmify.Repository.AdminRepository;

    public class AdminFAQRepository:IAdminFAQService
    {
        
        private readonly ApplicationDBContext _Context;

        public AdminFAQRepository(ApplicationDBContext context)
        {
            _Context = context;
        }
        public async Task<IEnumerable<FAQDto>> GetAllAsync(string? search = null, int pageNumber = 1, int pageSize = 10)
        {
          var query = _Context.FAQs.AsQueryable();
          if (!string.IsNullOrWhiteSpace(search)) query= query.Where
              (f=>f.Question.Contains(search)|| f.Answer.Contains(search));
          return await query.OrderByDescending(f => f.CreatedAt).Skip((pageNumber - 1) * pageSize)
              .Take(pageSize).Select(f => new FAQDto
              {
                  FaqId = f.FaqId,
                  Question = f.Question,
                  Answer = f.Answer,
                  CreatedAt = f.CreatedAt
              }).ToListAsync();

        }

        public async Task<FAQDto?> GetByIdAsync(int id)
        {
            var faq = await _Context.FAQs.FindAsync(id);
            if (faq == null) return null;
            return new FAQDto
            {
                FaqId = faq.FaqId,
                Question = faq.Question,
                Answer = faq.Answer,
                CreatedAt = faq.CreatedAt

            };
        }

        public async Task<FAQDto> CreateAsync(CreateFAQDto dto)
        {
            var faq = new FAQ
            {
                Question = dto.Question,
                Answer = dto.Answer,
                CreatedAt = DateTime.Now,
                IsActive = true
            };
             _Context.FAQs.Add(faq);
             await _Context.SaveChangesAsync();
             return await GetByIdAsync(faq.FaqId) ?? throw new Exception("FAQ creation failed");
        }

        public async Task<FAQDto> UpdateAsync(UpdateFAQDto dto)
        {
           var faq = await _Context.FAQs.FindAsync(dto.FaqId);
           if (faq == null) return null;
           if (!string.IsNullOrWhiteSpace(dto.Question)) faq.Question = dto.Question;
           if (!string.IsNullOrWhiteSpace(dto.Answer)) faq.Answer = dto.Answer;
           faq.UpdateAt = DateTime.Now;
           await _Context.SaveChangesAsync();
           return await GetByIdAsync(faq.FaqId);
        }

        public async Task<bool> ToogleActiveAsync(int id)
        {
            var faq = await _Context.FAQs.FindAsync(id);
            if (faq == null) return false;
            faq.IsActive = !faq.IsActive;
            await _Context.SaveChangesAsync();
            return true;
        }
        
        public async Task<bool> DeleteAsync(int id)
        {
            var faq = await _Context.FAQs.FindAsync(id);
            if (faq == null) return false;
            _Context.FAQs.Remove(faq);
             await _Context.SaveChangesAsync();
             return true;
            
        }
    }