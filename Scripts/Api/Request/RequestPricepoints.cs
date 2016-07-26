using SimpleJSON;

namespace Xsolla {
	public class RequestPricepoints : BaseWWWRequest {
		
		public RequestPricepoints(int type) : base(type){}
		
		protected override string GetMethod ()
		{
			return "/paystation2/api/pricepoints";
		}
		
		protected override object[] ParseResult (JSONNode rootNode)
		{
			var data = new XsollaPricepointsManager().Parse(rootNode) as XsollaPricepointsManager;
			return new object[]{data};
		}

	}
}
