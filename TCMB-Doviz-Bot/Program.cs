using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TCMB_Doviz_Bot
{
	class Program
	{
		static void Main(string[] args)
		{
			TcmbKurCek();
			Console.ReadLine();
		}

		private static void TcmbKurCek()
		{
			try
			{
				List<Doviz> DovizListesi = new List<Doviz>();
				XmlDocument doc = new XmlDocument();
				doc.Load("https://www.tcmb.gov.tr/kurlar/today.xml");
				XmlNodeList DovizPath = doc.SelectNodes("/Tarih_Date/Currency");

				foreach (XmlNode Doviz in DovizPath)
				{
					string TurkishName = Doviz.SelectSingleNode("Isim").InnerText;
					string CurrencyName = Doviz.SelectSingleNode("CurrencyName").InnerText;
					string ForexBuying = Doviz.SelectSingleNode("ForexBuying").InnerText;
					string ForexSelling = Doviz.SelectSingleNode("ForexSelling").InnerText;
					string BanknoteBuying = Doviz.SelectSingleNode("BanknoteBuying").InnerText;
					string BanknoteSelling = Doviz.SelectSingleNode("BanknoteSelling").InnerText;

					DovizListesi.Add(new Doviz()
					{
						TurkceKurADI = TurkishName,
						KurADI = CurrencyName,
						AlisFiyati = ForexBuying,
						SatisFiyati = ForexSelling,
						EfektikAlis = BanknoteBuying,
						EfektifSatis = BanknoteSelling
					});
				}
				Console.WriteLine("======================");
				foreach (var Doviz in DovizListesi)
				{
					Console.WriteLine($"Döviz Cinsi        :: {Doviz.TurkceKurADI}" + " / " + $"{Doviz.KurADI}");
					Console.WriteLine($"Döviz Alış         :: {Doviz.AlisFiyati}");
					Console.WriteLine($"Döviz Satış        :: {Doviz.SatisFiyati}");
					Console.WriteLine($"Banka Aliş Fiyati  :: {Doviz.EfektikAlis}");
					Console.WriteLine($"Banka Satiş Fiyati :: {Doviz.EfektifSatis}");
					Console.WriteLine("======================");
				}
			}
			catch (Exception msjver)
			{
				Console.WriteLine(msjver.Message);
			}
		}
	}
}
