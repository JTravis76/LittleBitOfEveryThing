using WebAppFunction.Api.Entities;
using WebAppFunction.Api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAppFunction.Api.Services.Interfaces;

public interface IGeneralService
{
  public Task<int> SaveDocumentAsync(Document document);

  public Task<Document> GetDocumentAsync(int id);
}