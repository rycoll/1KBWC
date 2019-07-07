//--------------------------------------
//               PowerUI
//
//        For documentation or 
//    if you have any issues, visit
//        powerUI.kulestar.com
//
//    Copyright � 2013 Kulestar Ltd
//          www.kulestar.com
//--------------------------------------

using System;


namespace Css.Properties{
	
	/// <summary>
	/// Represents the table-layout: css property.
	/// </summary>
	
	public class TableLayout:CssProperty{
		
		public static TableLayout GlobalProperty;
		
		
		public TableLayout(){
			GlobalProperty=this;
			InitialValue=AUTO;
		}
		
		public override string[] GetProperties(){
			return new string[]{"table-layout"};
		}
		
		public override ApplyState Apply(ComputedStyle style,Value value){
			
			// Ok!
			return ApplyState.Ok;
			
		}
		
	}
	
}



