using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day17
    {
        private const string InputPath = "input/2021-12-17.txt";

        public int Puzzle1()
        {
            var trajectoriesToTry = new List<ProbeTrajectory>();
            for (int x = 1; x < 100; x++)
            {
                for (int y = 1; y < 200; y++)
                {
                    trajectoriesToTry.Add(new ProbeTrajectory(x, y));
                }
            }

            return trajectoriesToTry.Where(probe => probe.DoesProbePassThroughTargetArea())
                                    .Max(probe => probe.GetMaxHeight());
        }

        private class ProbeTrajectory
        {
            private int _xPosition = 0;
            private int _yPosition = 0;
            private int _xVelocity;
            private int _yVelocity;
            private int _maxHeight;
            
            public ProbeTrajectory(int initialXVelocity, int initialYVelocity)
            {
                _xVelocity = initialXVelocity;
                _yVelocity = initialYVelocity;
                _maxHeight = 0;
            }

            public bool DoesProbePassThroughTargetArea()
            {
                while (!IsProbePastTargetArea())
                {
                    Step();
                    if (IsProbeInTargetArea())
                    {
                        return true;
                    }
                }

                return false;
            }

            private void Step()
            {
                _xPosition += _xVelocity;
                _yPosition += _yVelocity;
                _xVelocity = Math.Max(0, _xVelocity - 1);
                _yVelocity--;

                _maxHeight = Math.Max(_maxHeight, _yPosition);
            }

            private bool IsProbeInTargetArea()
            {
                return _xPosition is >= 79 and <= 137 && _yPosition is >= -176 and <= -117;
            }

            private bool IsProbePastTargetArea()
            {
                return _xPosition > 137 || _yPosition < -176;
            }

            public int GetMaxHeight()
            {
                return _maxHeight;
            }
        }
    }
}
