﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drutol.FigureRepository.Shared.Models.Figure;

public record FigureCheckoutDetails
{
    public decimal Price { get; init; }
}