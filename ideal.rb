#If the year has or not 1 week more
def days(y)
	y==((y*1.242189/7).round*7/1.242189).round ? 371 : 364
end
#Milliseconds from the counting to the glorious change of millennium 01/01/2000.
#The Gregorian 1/1/2000 would be 7/1/2000 in the ideal calendar, a standard I propose.
class Ideal
    include Comparable
    attr_accessor:D
	def initialize(g=Time.new)
		@D=0#Days since the change of millennium
		@y=2000#Year initiating in 2000
		@d=6#Day of the year counting since 0 (Sunday), 6 is Saturday, day the change of millennium was.
		addDays(((g-Time.local(2000, 1, 1))/86400).floor)#From Gregorian to ideal calendar
	end
	def addDays(n)
		@d+=n
		if n>0
			a=days(@y)
			while @d>=a
				@d-=a
				@y+=1
				a=days(@y)
			end
		else
			while @d<0
				@y-=1
				@d+=days(@y)
			end
		end
		@D+=n
	end
	def addYears(n)
		a=@y+n
		if @d>363
			if days(a)==364
				@d-=7
				@D-=7
			end
		end
		if n>0
			while @y<a
				@D+=days(@y)
				@y+=1
			end
		elsif n<0
			while @y>a
				@y-=1
				@D-=days(@y)
			end
		end
	end
    def <=>(o)
        self.D<=>o.D
    end
    def >(o)
        self.D>o.D
    end
    def <(o)
        self.D<o.D
    end
    def ==(o)
        self.D==o.D
    end
	#Day, week and month start at 0 internally and here 1 is added.
	def y
		@y
	end
	def m
		(@d/28)+1
	end
	def w
		(@d/7)+1
	end
	def d
		@d+1
	end
	def dm
		(@d%28)+1
	end
	def dw
		(@d%7)+1
	end
	def D
		@D
	end
	def gregorian
		Time.new(2000,1,1)+@D*86400
	end
	#Year and day. If d>days(y), days pass to some posterior year. If d<1, days pass to some previous year.
	def d=(v)
		addDays(v-@d-1)
	end
	def y=(v)
		addYears(v-@y)
	end
	@@f = true
	def to_s
		"#{(@@f!=true and @@f!=false) ? d : "#{@@f ? dm : dw} #{@@f ? m : w}"} #{y}"
	end
end
puts Ideal.new
