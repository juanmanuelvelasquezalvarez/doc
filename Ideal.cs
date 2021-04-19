using System;
namespace Ideal
{
	class Ideal
    	{
		//If the year has or not 1 week more
		public static int days(int y) => y == Math.Round(Math.Round(y * 1.242189 / 7) * 7 / 1.242189) ? 371 : 364;
		private int D,//Days since the change of millennium initiating in 0.
			y = 2000,//Year initiating in 2000
			d = 6;//Day of the year counting since 0 (Sunday), 6 is Saturday, day that the change of millennium was.
		//The Gregorian 1/1/2000 would be 7/1/2000 in the ideal calendar, a standard I propose.
		//From Gregorian to ideal calendar
		public Ideal(DateTime g)
		{
			AddDays((int)g.Subtract(DateTime.Parse("2000-01-01")).TotalDays);
		}
		//Year and day. If d>days(y), days pass to some posterior year. If d<1, days pass to some previous year.
		public Ideal(int y, int d)
		{
			AddYears(y - 2000); AddDays(d - 7);
		}
		public void AddDays(int n)
		{
			d += n;
			if (n > 0)
			{
				int N = days(y);
				while (d >= N)
				{
					d -= N;
					N = days(++y);
				}
			}
			else while (d < 0) d += days(--y);
			D += n;
		}
		public void AddYears(int n)
		{
			int a = y + n;
			if (d > 363) if (days(a) == 364)
				{
					d -= 7; D -= 7;
				}
			if (n > 0) while (y < a) D += days(y++);
			if (n < 0) while (y > a) D -= days(--y);
		}
		public static bool? f;
		public override string ToString()
		{
			if(f==null) return (d + 1) + " " + y;
			bool b = (bool)f;
			return (b?5:6)+" "+get(b?2:3)+" "+y;
		}
		//Choose among year, month, week, day of the year/month/week.
		//Day, week and month start at 0 internally and here 1 is added.
		public int get(int f) =>
			f < 1 | f > 6 ? 0 : f == 1 ? y : (f == 2 ? d / 28 : f == 3 ? d / 7 : f == 4 ? d : f == 5 ? d % 28 : d % 7) + 1;
		public static explicit operator int(Ideal i) => i.D;
		//If this date is previous or posterior than another.
		public static bool operator >(Ideal a, Ideal b) => a.D > b.D;
		public static bool operator <(Ideal a, Ideal b) => a.D < b.D;
		public static bool operator ==(Ideal a, Ideal b) => a.D == b.D;
		public static bool operator !=(Ideal a, Ideal b) => a.D != b.D;
		public static bool operator ==(Ideal a, int b) => a.D == b;
		public static bool operator !=(Ideal a, int b) => a.D != b;
		//From ideal to Gregorian calendar
		public static explicit operator DateTime(Ideal i) => DateTime.Parse("2000-01-01").AddDays(i.D);
		public static explicit operator string(Ideal i) => i.ToString();
		static void Main(string[] args)
		{
			Console.WriteLine(new Ideal(DateTime.Now));
		}
		public override bool Equals(object o) => this==(Ideal)o;
		public override int GetHashCode() => base.GetHashCode();
	}
}
