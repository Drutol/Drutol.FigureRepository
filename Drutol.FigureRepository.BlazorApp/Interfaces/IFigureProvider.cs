﻿using Drutol.FigureRepository.Shared.Models.Figure;

namespace Drutol.FigureRepository.BlazorApp.Interfaces;

public interface IFigureProvider
{
    TaskCompletionSource DataReady { get; }
    List<Figure> Figures { get; }
    ValueTask Initialize();
}