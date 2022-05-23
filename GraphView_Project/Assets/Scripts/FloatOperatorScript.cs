using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class FloatOperatorScript : Unit {
	[DoNotSerialize] public ControlInput inputTrigger;
	[DoNotSerialize] public ControlOutput outputTrigger;
	
	[DoNotSerialize] public ValueInput _a;
	[DoNotSerialize] public ValueInput _b;
	[DoNotSerialize] public ValueInput _c;
	[DoNotSerialize] public ValueInput _waitSeconds;
	[DoNotSerialize] public ValueOutput result;

	private float resultValue;
	
	protected override void Definition() {
		inputTrigger = ControlInputCoroutine("inputTrigger", RunCoroutine);
		outputTrigger = ControlOutput("outputTrigger");
		
		_a = ValueInput<float>("_a", 0);
		_b = ValueInput<float>("_b", 0);
		_c = ValueInput<float>("_c", 0);
		_waitSeconds = ValueInput<float>("_waitSeconds", 0);
		result = ValueOutput("result", (flow) => resultValue);
		
		Succession(inputTrigger, outputTrigger);
	}

	private IEnumerator RunCoroutine(Flow flow) {
		float aValue = flow.GetValue<float>(_a);
		float bValue = flow.GetValue<float>(_b);
		float cValue = flow.GetValue<float>(_c);
		yield return new WaitForSeconds(flow.GetValue<float>(_waitSeconds));
		resultValue = aValue + bValue - cValue;
		yield return outputTrigger;
	}
}
