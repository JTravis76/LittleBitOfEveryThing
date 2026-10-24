using AutoMapper;
using WebAppFunction.Api.Classes;
using WebAppFunction.Api.Database;
using WebAppFunction.Api.Entities;
using WebAppFunction.Api.Enums;
using WebAppFunction.Api.Extensions;
using WebAppFunction.Api.Models;
using WebAppFunction.Api.Services.Interfaces;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebAppFunction.Api.Services;

public class GeneralService : IGeneralService
{
  private readonly DataContext _dataContext;
  private readonly ILogger<CircuitService> _logger;
  private readonly IMapper _mapper;

  public GeneralService(
      DataContext dataContext,
      ILogger<GeneralService> logger,
      IMapper mapper)
  {
    _dataContext = dataContext;
    _logger = logger;
    _mapper = mapper;
  }

  public async Task<int> SaveDocumentAsync(Document document)
  {
    _dataContext.Add(document);
    return await _dataContext.SaveChangesAsync();
    //return await Task.Run(() => 0);
  }

  public async Task<Document> GetDocumentAsync(int id)
  {
    return await _dataContext.Documents.FirstOrDefaultAsync(x => x.Id == id);
  }

}