using System.Collections.Generic;
using System.Linq;

public sealed class RagdollState : IRagdollState
{
    public sealed class RagdollBlendedState : IRagdollState
    {
        RagdollState a;
        RagdollState b;

        public RagdollBlendedState(RagdollState a, RagdollState b)
        {
            this.a = a;
            this.b = b;
        }

        public float Blend { get; set; }

        public void ApplyStateToElement(IRagdollElement element)
        {
            var aState = a.elementStateMap[element];
            var bState = b.elementStateMap[element];
            var blended = RagdollElementState.Lerp(aState, bState, Blend);
            element.WriteState(blended);
        }
    }

    Dictionary<IRagdollElement, RagdollElementState> elementStateMap;
    
    public RagdollState(IEnumerable<IRagdollElement> elements)
    {
        elementStateMap = elements.ToDictionary(e => e, e => e.ReadState());
    }

    public void ApplyStateToElement(IRagdollElement element)
    {
        element.WriteState(elementStateMap[element]);
    }

    public RagdollBlendedState CreateBlended(RagdollState blendTo)
    {
        return new RagdollBlendedState(this, blendTo);
    }
}
