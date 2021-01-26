import time
from datetime import datetime
#If the year has or not 1 week more
def days(y): return 371 if y==round(round(y*1.242189/7)*7/1.242189) else 364
#Milliseconds from the counting to the glorious change of millennium: 01/01/2000.
#The Gregorian 1/1/2000 would be 7/1/2000 in the ideal calendar, a standard I propose.
ts=datetime(2000, 1, 1).timestamp()
f=True
class ideal:
	def __init__(self, y=None, d=1, g=time.time()):
		self._D=0#Days since the change of millennium
		self._y=2000#Year initiating in 2000
		self._d=6#Day of the year counting since 0 (Sunday), 6 is Saturday, day the change of millennium was.
		if y is None: self.addDays(int((g-ts)/86400))#From Gregorian to ideal calendar
		else:
			self.addYears(y-2000)
			#If d>days(y), days pass to some posterior year. If d<1, days pass to some previous year.
			self.addDays(d-7)
	def addDays(self,n):
		if not isinstance(n, int): return NotImplemented
		self._d+=n
		if n>0:
			N=days(self._y)
			while self._d>=N:
				self._d-=N
				self._y+=1
				N=days(self._y)
		else:
			while self._d<0:
				self._y-=1
				self._d+=days(self._y)
		self._D+=n
	def addYears(self,n):
		if not isinstance(n, int): return NotImplemented
		a=self._y+n
		if self._d>363:
			if days(a)==364:
				self._d-=7
				self._D-=7
		if n>0:
			while self._y<a:
				self._D+=days(self._y)
				self._y+=1
		if n<0:
			while self._y>a:
				self._y-=1
				self._D-=days(self._y)
	#Choose among year, month, week, day of the year/month/week.
	#Day, week and month start at 0 internally and here 1 is added.
	@property
	def y(self): return self._y
	@property
	def m(self): return int(self._d/28)+1
	@property
	def w(self): return int(self._d/7)+1
	@property
	def d(self): return self._d
	@property
	def dm(self): return self._d%28+1
	@property
	def dw(self): return self._d%7+1
	@property
	def D(self): return self._D
	def __eq__(self, o):
		if self==o: return True
		if isinstance(o, int): return self._D==o
		if isinstance(o, ideal): return self._D==o.D
		return False
	def __le__(self, o):
		if self is o: return True
		if isinstance(o, int): return self._D<=o
		if isinstance(o, ideal): return self._D<=o.D
		return False
	def __lt__(self, o):
		if self==o: return True
		if isinstance(o, int): return self._D<o
		if isinstance(o, ideal): return self._D<o.D
		return False
	def __ge__(self, o):
		if self==o: return True
		if isinstance(o, int): return self._D>=o
		if isinstance(o, ideal): return self._D>=o.D
		return False
	def __gt__(self, o):
		if self==o: return True
		if isinstance(o, int): return self._D>o
		if isinstance(o, ideal): return self._D>o.D
		return False
	#Day of the year, day (1 to 28) and month or day (1 to 7) and week and the year
	def __str__(self): return (str(self._d) if f==None else str(self.dm if f else self.dw)+" "+str(self.m if f else self.w))+" "+str(self._y)
	#Ideal calendar to Gregorian from milliseconds.
	def gregorian(self): return datetime.fromtimestamp(self._D*86400+ts)
print(ideal())
