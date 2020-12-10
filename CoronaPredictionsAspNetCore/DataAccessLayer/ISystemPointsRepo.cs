using CoronaPredictionsAspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaPredictionsAspNetCore.DataAccessLayer
{
    public interface ISystemPointsRepo
    {
        List<PointSystem> AllSystemPoints();

    }
}
