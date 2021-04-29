using System.Collections.Generic;

namespace paautoauctionapp.Shared.Utils
{

    public class MileageBounds
    {

        public static readonly List<MileageBound> Bounds = new List<MileageBound>
        {
            new MileageBound(null, 10000),
            new MileageBound(10000, 20000),
            new MileageBound(20000, 30000),
            new MileageBound(30000, 40000),
            new MileageBound(40000, 50000),
            new MileageBound(50000, 60000),
            new MileageBound(60000, 70000),
            new MileageBound(70000, 80000),
            new MileageBound(80000, 90000),
            new MileageBound(90000, 100000),
            new MileageBound(100000, 110000),
            new MileageBound(110000, 120000),
            new MileageBound(120000, 130000),
            new MileageBound(130000, 140000),
            new MileageBound(140000, 150000),
            new MileageBound(150000, 160000),
            new MileageBound(160000, 170000),
            new MileageBound(170000, 180000),
            new MileageBound(180000, 190000),
            new MileageBound(190000, 200000),
            new MileageBound(200000, 210000),
            new MileageBound(210000, 220000),
            new MileageBound(220000, 230000),
            new MileageBound(230000, 240000),
            new MileageBound(240000, 250000),
            new MileageBound(250000, null),
        };

        public static int GetMileageBoundId(int mileage)
        {

            for (int i = 0; i < Bounds.Count; i++)
            {
                if (mileage >= Bounds[i].LowerBound & mileage < Bounds[i].UpperBound)
                {
                    return i;
                }
            }

            if (mileage < Bounds[0].UpperBound)
            {
                return 0;
            }

            if (mileage >= Bounds[1].LowerBound)
            {
                return Bounds.Count - 1;
            }

            return -1;

        }

    }

}
