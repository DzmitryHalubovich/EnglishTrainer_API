﻿using EnglishTrainer.Repositories.Interfaces;
using EnglishTrainer.Entities;
using EnglishTrainer.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EnglishTrainer.Repositories.Implemintations
{
    public class IrregularVerbRepository : RepositoryBase<IrregularVerb>, IIrregularVerbRepository
    {
        public IrregularVerbRepository(EFContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<IrregularVerb>> GetVerbsAsync(bool trackChanges) =>
            await FindAll(trackChanges).ToListAsync();
    }
}
