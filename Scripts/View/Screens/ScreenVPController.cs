﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Xsolla {

	public class ScreenVPController : ScreenBaseConroller<XVirtualPaymentSummary> {
		
		public Text Title;
		public Text Confirmation;
		public GameObject ErrorContainer;
		public Text Error;
		public Text ItemName;
		public Text BackTo;
		public Text Total;
		public Text ProceedButtonText;
		public Text ToggleText;
		public Button ProceedButton;
		public Toggle Toggle;
		public ImageLoader ImageLoader;

		public override void InitScreen(XsollaTranslations translations, XVirtualPaymentSummary summary) {
		}

		public void DrawScreen(XsollaUtils utils, XVirtualPaymentSummary summary) {
			ResizeToParent ();
			Title.text = utils.GetTranslations ().Get ("cart_page_title");
			Confirmation.text = utils.GetTranslations ().Get ("cart_confirm_your_purchase");
			ImageLoader.UploadImageToCurrentView (summary.Items [0].GetImage());
			ItemName.text = summary.Items [0].Name;
			BackTo.text = "< " + utils.GetTranslations ().Get ("back_to_virtualitem");
			ToggleText.text = utils.GetTranslations ().Get ("cart_dont_ask_again");
			Total.text = utils.GetTranslations ().Get ("total") + " " + summary.Total + " " + utils.GetProject().virtualCurrencyName;
			ProceedButtonText.text = utils.GetTranslations ().Get ("cart_submit");
			ProceedButton.onClick.AddListener (() => OnClickProceed());

		}

		public void ShowError(string errorText) {
			Error.gameObject.transform.parent.gameObject.SetActive(true);
			Error.text = errorText;
		}

		private void OnClickProceed() {
			Logger.Log ("Toggle " + Toggle.isOn);
			int isRemeber = 0;
			if (Toggle.isOn)
				isRemeber = 1;
			Dictionary<string, object> purchase = new Dictionary<string, object> (1);
			purchase.Add("dont_ask_again", isRemeber);
			gameObject.GetComponentInParent<XsollaPaystationController> ().ProceedVirtualPayment (purchase);
		}

	}

}
