package a;
import java.util.Date;
@SuppressWarnings({"deprecation","preview"})
public class Ideal implements Comparable<Ideal> {
	/*Milliseconds from the counting (1st second of 1970 Greenwich)
	  to the glorious change of millennium: 01/01/2000.
	  In the class Date, month starts at 0 and year=1900+parameter*/
	private static long m=new Date(100,0,1).getTime();
	//If the year has or not 1 week more
	public static int days(int y) {
		return y==Math.round(Math.round(y*1.242189/7)*7/1.242189)?371:364;
	}
	private int D,//Days since the change of millennium initiating in 0.
		y=2000,//Year initiating in 2000
		d=6;//Day of the year counting since 0 (Sunday), 6 is Saturday, day that the change of millennium was.
	//The Gregorian 1/1/2000 would be 7/1/2000 in the ideal calendar, a standard I propose.
	public Ideal() {//Now
		this(System.currentTimeMillis());
	}
	//From Gregorian to ideal calendar
	public Ideal(Date g) {
		this(g.getTime());
	}
	public Ideal(long g) {
		addDays((int)((g-m)/86400000));
	}
	//Year and day.
	//If d>days(y), days pass to some posterior year.
	//If d<1, days pass to some previous year.
	public Ideal(int y, int d) {
		addYears(y-2000);addDays(d-7);
	}
	//Year, month or week according to the boolean and day
	public Ideal(int y, int m, int d, boolean b) {
		this(y,(m-1)*(b?28:7)+d);
	}
	public void addDays(int n) {
		d+=n;
		if(n>0) {
			int N=days(y);
			while(d>=N) {
				d-=N;
				N=days(++y);
			}
		}
		else while(d<0) d+=days(--y);
		D+=n;
	}
	public void addYears(int n) {
		int a=y+n;
		if(d>363) if(days(a)==364) {
			d-=7;D-=7;
		}
		if(n>0) while(y<a) D+=days(y++);
		if(n<0) while(y>a) D-=days(--y);
	}
	//Choose among year, month, week, day of the year/month/week.
	//Day, week and month start at 0 internally and here 1 is added.
	public int get(int f) {
		return f<1|f>6?0:f==1?y:(f==2?d/28:f==3?d/7:f==4?d:f==5?d%28:d%7)+1;
	}
	//If this date is previous or posterior than another.
	public int compareTo(Ideal f) {
		return D-f.D;
	}
	public boolean equals(Object o) {
		if(this==o) return true;
		if(o instanceof Date d) return D==new Ideal(d).D;
		if(o instanceof Ideal i) return D==i.D;
		return false;
	}
	//Day of the year, day (1 to 28) and month or day (1 to 7) and week and the year
	public Boolean f=true;
	public String toString() {
		return (f==null?d:get(f?5:6)+" "+get(f?2:3))+" "+y;
	}
	//Ideal calendar to Gregorian from milliseconds.
	public Date gregorian() {
		return new Date((long)D*86400000+m);
	}
	public Ideal clone() {
		return new Ideal(y,d);
	}
	public static void main(String[] a) {
		System.out.print(new Ideal());
	}
}
