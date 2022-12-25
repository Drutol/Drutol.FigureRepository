using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drutol.FigureRepository.Shared.Models.Figure.Enums;

namespace Drutol.FigureRepository.Shared.Models.Figure;

public record FigureExternalPurchaseOption(FigureExternalPurchaseOptionType Type, string Link, bool RequiresWallet);