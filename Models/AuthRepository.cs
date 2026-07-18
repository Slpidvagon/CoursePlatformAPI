using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace AuuthRepository
{
    public class AuthRepository
    {
        private readonly Context _db;

        public AuthRepository(Context db)
        {
            _db = db;
        }

        public async Task<IEnumerable<AuthorEntity>> GetAllAuthsync()
        {
            return await _db.Authors
                .Include(c=>c.Course)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<AuthorEntity> CreateAuth(AuthorEntity author)
        {
            author.Id = Guid.NewGuid();
            _db.Authors.Add(author);
            _db.SaveChangesAsync();
            return author;

        }

        public async Task<AuthorEntity?> GetByIdAuthAsync(Guid id)
        {
            return await _db.Authors
                .Include(c => c.Course)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

        }
       

        public async Task<bool> DeleteAuthAsync(Guid id)
        {
            var authors = await _db.Authors.FindAsync(id);
            if (authors is null) return false;
            _db.Authors.Remove(authors);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<AuthorEntity?> UpdateAsync(AuthorEntity author)
        {

            var existing = await _db.Authors.FindAsync(author.Id);


            if (existing is null) return null;


            existing.Name = author.Name;
            

          
            await _db.SaveChangesAsync();


            return existing;
        }



    }
}
