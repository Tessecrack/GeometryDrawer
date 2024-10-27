using Assets.GoemetryDrawer.Scripts.Utils;
using System;
using System.Collections.Generic;

namespace Assets.GoemetryDrawer.Scripts.Services.Figures
{
    public class FiguresContainer
    {
        private Dictionary<string, BaseMesh> IdFigures = new Dictionary<string, BaseMesh>();


        public void AddFigure(BaseMesh figure)
        {
            if (IdFigures.ContainsKey(figure.Id))
            {
                throw new Exception($"Error. Figure with id {figure} was already added");
            }
            IdFigures.Add(figure.Id, figure);
        }

        public void RemoveFigure(BaseMesh figure)
        {
            if (!IdFigures.ContainsKey(figure.Id))
            {
                throw new Exception($"Error. Figure with id {figure} was already removed");
            }
            IdFigures.Add(figure.Id, figure);
        }
    }
}
