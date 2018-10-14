using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacconsLibraryCommon
{

    /// <summary>
    /// Commented example. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    //public class EqualityOvverideForValueTypes<T> : IEquatable<T> // (1) first implement IEquatable<>
    //{
    //    //public bool Equals(T other)
    //    //{
    //    //    return this._name == other.Name && this._group == other._group; 
        //}

        //public override bool Equals(object obj) // (2) ovveride Object.Equals();
        //{
        //    /*if (obj is MyValue) // first check type
        //        return Equals((MyValue)obj); // then call IEquateble<T>.Equals();
        //    else
        //        return false;*/           
        //}

        //public static bool operator == (MyClass lhsm, MyClass rhs) //(3) override == operator
        //{
        //    return lhs.Equals(rhs); // we are calling IEquatable<T>.Eqauls()
        //}

        //public static bool operator !=(MyClass lhsm, MyClass rhs) //(4) override != operator
        //{
        //    return !lhs.Equals(rhs); // we are calling IEquatable<T>.Eqauls()
        //    alternative:
        //    return !(lhs == rhs);
        //}

        //public override int GetHashCode() //(5) ovveride GetHashCode
        //{
        //    //return _name.GetHashCode() ^ _group.GetHashCode(); // ^ - is exclusive or --> XOR operator
        //}
    //}

    /// <summary>
    /// Commented example. 
    /// </summary>
    public class EqualityOvverideForRefTypes
    {
        //public override bool Equals(object obj) //(1) override Object.Eqauls()
        //{
        //    //if (obj == null)
        //    //    return false;
        //    //if (ReferenceEquals(obj, this))
        //    //    return true;
        //    //if (obj.GetType() != this.GetType())
        //    //    return false;
        //    //MyClass rhs = obj as MyClass;
        //    //return this._someField == rhs._someField && this._otherField == rhs._otherField;
        //    return true;
        //}

        //public override int GetHashCode() //(2) override GetHashCode
        //{
        //    return this.someField.GetHashCode() ^ this.otherField.GetHashCode();
        //}

        //public static bool operator == (MyClass x, MyClass y)//(3) override ==. Static object.Equals()does bool-checking then calls virtual Equals();
        //{
        //    return object.Equals(x, y);
        //}

        //public static bool operator != (MyClass x, MyClass y)//(4) override !=. Static object.Equals()does bool-checking then calls virtual Equals();
        //{
        //    return !object.Equals(x, y);
        //}

    }
    
    /// <summary>
    /// Commented example. 
    /// </summary>
    public class ChildOfEqualityOvverideForRefTypes : EqualityOvverideForRefTypes // example how to override equaliy in inherited class
    {
        //public override bool Equals(object obj) //(1) override Object.Eqauls()
        //{
        //    //if (!base.Equals(obj))
        //    //    return false;
        //    //ThisClass rhs = (ThisClass) obj;
        //    //return this._someField == rhs._someField; and check if derived type fields are equal
        //    
        //}

        //public override int GetHashCode() //(2) override GetHashCode
        //{
        //    return base.someField.GetHashCode() ^ this.otherField.GetHashCode();
        //}

        //public static bool operator == (DerivedClass x, DerivedClass y)//(3) override ==. Static object.Equals()does bull-checking then calls virtual Equals();
        //{
        //    return object.Equals(x, y); // the same as in the base class
        //}

        
        //public static bool operator != (DerivedClass x, DerivedClass y)//(4) override !=. Static object.Equals()does bull-checking then calls virtual Equals();
        //{
        //    return !object.Equals(x, y); // the same as in the base class
        //}
    }
}


