/*
 * RADO OSREDKAR
 * 10.03.2011
*/

using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace Math.Tools.TrackReaders
{
    public class PointLineKml
    {
        private readonly Hashtable KMLCollection = new Hashtable(); //parsed KML
        private readonly List<Hashtable> LinesCollection = new List<Hashtable>(); //all parsed kml lines

        //return types
        private readonly List<Hashtable> PointsCollection = new List<Hashtable>(); //all parsed kml points

        private kmlGeometryType? currentGeometry; //currently parsed geometry object
        private kmlTagType? currentKmlTag; //currently parsed kml tag

        private string lastError;
        private Hashtable Line; //single line (part of LinesCollection)
        private Hashtable Point; //single point (part of PointsCollection)

        /// <summary>
        /// Last returned error
        /// </summary>
        public string LastError
        {
            get => lastError;
            set
            {
                //remember error and promote it to caller
                lastError = value;
                throw new System.Exception(lastError);
            }
        }

        /// <summary>
        /// parse kml, fill Points and Lines collections
        /// </summary>
        /// <param name="fileName">Full ABSOLUTE path to file.</param>
        /// <returns>HashTable</returns>
        public Hashtable KMLDecode(string fileName)
        {
            readKML(fileName);
            if (PointsCollection != null) KMLCollection.Add("POINTS", PointsCollection);
            if (LinesCollection != null) KMLCollection.Add("LINES", LinesCollection);
            return KMLCollection;
        }

        /// <summary>
        /// Open kml, loop it and check for tags.
        /// </summary>
        /// <param name="fileName">Full ABSOLUTE path to file.</param>
        private void readKML(string fileName)
        {
            using (var kmlread = XmlReader.Create(fileName))
            {
                while (kmlread.Read()) //read kml node by node
                {
                    //select type of tag and object
                    switch (kmlread.NodeType)
                    {
                        case XmlNodeType.Element:
                            //in elements select kml type
                            switch (kmlread.Name.ToUpper())
                            {
                                case "POINT":
                                    currentGeometry = kmlGeometryType.POINT;
                                    Point = new Hashtable();
                                    break;
                                case "LINESTRING":
                                    currentGeometry = kmlGeometryType.LINESTRING;
                                    Line = new Hashtable();
                                    break;
                                case "COORDINATES":
                                    currentKmlTag = kmlTagType.COORDINATES;
                                    break;
                            }

                            break;
                        case XmlNodeType.EndElement:
                            //check if any geometry is parsed in add it to collection
                            switch (kmlread.Name.ToUpper())
                            {
                                case "POINT":
                                    if (Point != null) PointsCollection.Add(Point);
                                    //Reinit vars
                                    Point = null;
                                    currentGeometry = null;
                                    currentKmlTag = null;
                                    break;
                                case "LINESTRING":
                                    if (Line != null) LinesCollection.Add(Line);
                                    //Reinit vars
                                    Line = null;
                                    currentGeometry = null;
                                    currentKmlTag = null;
                                    break;
                            }

                            break;
                        case XmlNodeType.Text:
                        case XmlNodeType.CDATA:
                        case XmlNodeType.Comment:
                        case XmlNodeType.XmlDeclaration:
                            //Parse inner object data
                            switch (currentKmlTag)
                            {
                                case kmlTagType.COORDINATES:
                                    parseGeometryVal(kmlread.Value); //try to parse coordinates
                                    break;
                            }

                            break;
                        case XmlNodeType.DocumentType:
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Parse selected geometry based on type
        /// </summary>
        /// <param name="tag_value">Value of geometry element.</param>
        protected void parseGeometryVal(string tag_value)
        {
            switch (currentGeometry)
            {
                case kmlGeometryType.POINT:
                    parsePoint(tag_value);
                    break;
                case kmlGeometryType.LINESTRING:
                    parseLine(tag_value);
                    break;
            }
        }

        /// <summary>
        /// If geometry is point select element values:
        ///     COORDINATES - add lat and lan to HashTable Point
        /// </summary>
        /// <param name="tag_value">Value of geometry element.</param>
        protected void parsePoint(string tag_value)
        {
            switch (currentKmlTag)
            {
                case kmlTagType.COORDINATES:
                    //kml point coordinates format is [lat,lan]
                    var value = new Hashtable();
                    var coordinates = tag_value.Split(',');
                    if (coordinates.Length < 2) lastError = "ERROR IN FORMAT OF POINT COORDINATES";
                    value.Add("LNG", coordinates[0].Trim());
                    value.Add("LAT", coordinates[1].Trim());
                    Point.Add("COORDINATES", value);
                    break;
            }
        }

        /// <summary>
        /// If geometry is line select element values:
        ///     COORDINATES - add lat and lan to List
        ///                   add list to HashTable Line
        /// </summary>
        /// <param name="tag_value">Value of geometry element.</param>
        protected void parseLine(string tag_value)
        {
            switch (currentKmlTag)
            {
                case kmlTagType.COORDINATES:
                    //kml coordinates format is [lat,lan]
                    var value = new List<Hashtable>();
                    var vertex = tag_value.Trim().Split(' ');

                    foreach (var point in vertex)
                    {
                        var coordinates = point.Split(',');
                        if (coordinates.Length < 2) LastError = "ERROR IN FORMAT OF LINESTRING COORDINATES";
                        foreach (var _ in coordinates)
                        {
                            var linePoint = new Hashtable {{"LNG", coordinates[0]}, {"LAT", coordinates[1]}};
                            value.Add(linePoint);
                        }
                    }

                    Line.Add("COORDINATES", value); //Add coordinates to line
                    break;
            }
        }

        //kml geometry
        private enum kmlGeometryType
        {
            POINT,
            LINESTRING
        }

        //kml tags
        private enum kmlTagType
        {
            POINT,
            LINESTRING,
            COORDINATES
        }
    }
}