namespace paautoauctionapp.Shared.Utils
{

    public class MileageBound
    {

        public int? LowerBound { get; }
        public int? UpperBound { get; }

        public MileageBound(int? lowerBound, int? upperBound)
        {
            LowerBound = lowerBound;
            UpperBound = upperBound;
        }

        public override string ToString()
        {

            if (UpperBound == null)
            {
                return $"More than {LowerBound:N0}";
            }
            else if (LowerBound == null)
            {
                return $"Less than {UpperBound:N0}";
            }

            return $"{LowerBound:N0}-{UpperBound:N0}";
        }

    }

}