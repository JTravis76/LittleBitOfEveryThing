using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppFunction.Api.Models;

namespace WebAppFunction.Api.Services.Interfaces
{
    public interface IGraphService
    {
        public Task<GraphUserModel> Users();
    }
}
