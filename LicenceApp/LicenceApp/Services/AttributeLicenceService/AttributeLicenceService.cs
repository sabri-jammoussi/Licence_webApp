using LicenceApp.Data;
using LicenceApp.Enums;
using LicenceApp.models;
using LicenceApp.models.AttributeLicence;
using LicenceApp.models.GlobalDao;
using Microsoft.EntityFrameworkCore;

namespace LicenceApp.Services.AttributeLicenceService
{
    public class AttributeLicenceService : IAttributeLicenceService
    {
        private readonly LicenceDBContext _dbContext;

        public AttributeLicenceService(LicenceDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(NewAttributeLicence newAttributeLicence)
        {
            var data = new AttributeLicenceDao
            {
                Intutile = newAttributeLicence.Intutile,
                Description = newAttributeLicence.Description,
                Type = newAttributeLicence.Type,
                Obligations = newAttributeLicence.Obligations,

                ApplicationId = newAttributeLicence.ApplicationId,
                EnumurationValue = newAttributeLicence.EnumerationId,
            };
            _dbContext.attributeLicences.Add(data);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var attributeLicence = await _dbContext.attributeLicences.SingleOrDefaultAsync(u => u.Id == id);
            if (attributeLicence == null)
                throw new ApplicationException("Attribute id n'existe pas ");
            _dbContext.attributeLicences.Remove(attributeLicence);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<AttributeLicenceDto>> GetAll()
        {
            List<AttributeLicenceDto> result = await _dbContext.attributeLicences
          .Select(u => new AttributeLicenceDto
          {
              Id = u.Id,
              Intutile = u.Intutile,
              Description = u.Description,
              Type = u.Type,
              Obligations = u.Obligations,
                EnumerationId = u.EnumurationValue,
          }).ToListAsync();
            return result;
        }
        
        public async Task<AttributeLicenceDto> GetById(int id)
        {
            var attribute = await _dbContext.attributeLicences.SingleOrDefaultAsync(u => u.Id == id);
            if (attribute == null)
                throw new ApplicationException($"the id : {id} n'existe pas ");
            return new AttributeLicenceDto
            {
                Id = attribute.Id,
                Description = attribute.Description,
                Type = attribute.Type,
                Intutile = attribute.Intutile,
                Obligations =   attribute.Obligations,
                EnumerationId = attribute.EnumurationValue,

            };

        }

        public async Task Update(UpdateAttributeLicence updateAttributeLicence)
        {
            var existingAttribute = await _dbContext.attributeLicences.FirstOrDefaultAsync(u => u.Id == updateAttributeLicence.Id);
            if (existingAttribute == null)
                throw new ApplicationException($"the id : {updateAttributeLicence.Id} n'existe pas ");
            existingAttribute.Intutile = updateAttributeLicence.Intutile;
            existingAttribute.Description = updateAttributeLicence.Description;
            existingAttribute.Type = updateAttributeLicence.Type;
            existingAttribute.EnumurationValue = updateAttributeLicence.EnumerationId;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<AttributeLicenceDto>> GetAttributeValue(int id)
        {
            var attributeVals = await _dbContext.attributeLicences.Where(u => u.ApplicationId == id).ToListAsync();
            //if (attributeVals == null || attributeVals.Count == 0)
            //    throw new ApplicationException($"wrong id {id}");
            var attributeValDto = attributeVals.Select(attributeVals => new AttributeLicenceDto
            {
                Id = attributeVals.Id,
                Description = attributeVals.Description,
                Intutile = attributeVals.Intutile,
                Type = attributeVals.Type,
                Obligations = attributeVals.Obligations,
                EnumerationId = attributeVals.EnumurationValue,
            }).ToList();
            return attributeValDto;
        }

        public async Task<AttributeType> GetAttributeType()
        {
            try
            {
                var allTypes = Enum.GetNames(typeof(Types)).ToList();
                var attributeType = new AttributeType
                {
                    types = allTypes
                };

                return attributeType;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Type n'existe pas");
            }
        }
    }
}
