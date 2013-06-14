using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.GlobeCore;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesGDB;



namespace EngineApplication
{
    
    public class DotRender
    {
        IGeoFeatureLayer pGeoLayer;

        IDotDensityRenderer pDotDensityRenderer;//��Ⱦ����

        IDotDensityFillSymbol pDotDensityFill;//��Ⱦ�����Ŷ��󣬴����ֽ�С���󣬶����Ŀɿ�������

        IRendererFields pRendFields;//���Ǹ��ֶ���Ⱦ������ι�ϵ��

        ISymbolArray pSymbolArry;

        public DotRender(AxMapControl pMapControl, IFeatureLayer pFtLayer, double pValue,string pFieldName)
        {



            //IActiveView pActiveView;

            //this.pGeoLayer = pFtLayer as IGeoFeatureLayer;

            //pActiveView = pMapControl.ActiveView;

            //pDotDensityRenderer = new DotDensityRendererClass();

            //pRendFields = pDotDensityRenderer as IRendererFields;

            //pRendFields.AddField(pFieldName, pFieldName); //ͬһ������Ľӿڵ��л����ܷ���ġ�

            //this.pDotDensityFill = new DotDensityFillSymbolClass();

            //pDotDensityFill.DotSize = 8;

            //pDotDensityFill.Color = GetRGBColor(10, 20, 0);

            //pDotDensityFill.BackgroundColor = GetRGBColor(100, 108, 190);

            //pSymbolArry = pDotDensityFill as ISymbolArray;//�ѵ����ܶȡ�

            //ISimpleMarkerSymbol pSimpleMark;

            //pSimpleMark = new SimpleMarkerSymbolClass();

            //pSimpleMark.Style = esriSimpleMarkerStyle.esriSMSDiamond;

            //pSimpleMark.Size = 8;

            //pSimpleMark.Color = GetRGBColor(128, 128, 255);

            //pSymbolArry.AddSymbol(pSimpleMark as ISymbol);

            //pDotDensityRenderer.DotDensitySymbol = pDotDensityFill;

            //pDotDensityRenderer.DotValue = pValue;

            //pDotDensityRenderer.CreateLegend();
            //pGeoLayer.Renderer = (IFeatureRenderer)pDotDensityRenderer;

            //pActiveView.Refresh();




            //��ȡ��ǰͼ�� �����������ó�IGeoFeatureLayer��ʵ�� 
            IMap pMap = pMapControl.Map;
            // ILayer pFtLayer = pMap.get_Layer(0) as IFeatureLayer;
            IFeatureLayer pFeatureLayer = pFtLayer as IFeatureLayer;
            IGeoFeatureLayer pGeoFeatureLayer = pFtLayer as IGeoFeatureLayer;

            //��ȡͼ���ϵ�feature
            IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
            IFeatureCursor pFeatureCursor = pFeatureClass.Search(null, false);
            IFeature pFeature = pFeatureCursor.NextFeature();

            ///////////////////////

            ///////////////////////////////////////////////////////////////////
            //������ܶ�ͼ��Ⱦ���
            IDotDensityRenderer DotDensityRenderer = new DotDensityRendererClass();

            //������ܶ�ͼ��Ⱦ����������Ⱦ�ֶζ���
            IRendererFields flds = (IRendererFields)DotDensityRenderer;
            flds.AddField(pFieldName, pFieldName);
            //flds.AddField("Shape", "Shape");

            //������ܶ�ͼ��Ⱦ�÷��Ŷ���
            IDotDensityFillSymbol ddSym = new DotDensityFillSymbolClass();
            IRgbColor BackColor = new RgbColorClass();
            BackColor.Red = 234;
            BackColor.Blue = 128;
            BackColor.Green = 220;
            IRgbColor SymbolColor = new RgbColorClass();
            SymbolColor.Red = 234;
            SymbolColor.Blue = 128;
            SymbolColor.Green = 220;
            ////���ܶ�ͼ��Ⱦ������ɫ
            //ddSym.BackgroundColor = BackColor;
            ddSym.DotSize = 8;
            ddSym.FixedPlacement = true;
            //ddSym.Color = SymbolColor;
            ILineSymbol pLineSymbol = new CartographicLineSymbolClass();
            ddSym.Outline = pLineSymbol;

            //����������� 
            ISymbolArray symArray = (ISymbolArray)ddSym;
            //��ӵ��ܶ�ͼ��Ⱦ�ĵ���ŵ�����������ȥ
            ISimpleMarkerSymbol pMarkerSymbol = new SimpleMarkerSymbolClass();
            pMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle;
            pMarkerSymbol.Size = 0.2;
            pMarkerSymbol.Color = SymbolColor; ;


            symArray.AddSymbol(pMarkerSymbol as ISymbol);

            //���õ��ܶ�ͼ��Ⱦ�ĵ����
            //DotDensityRenderer.DotDensitySymbol =symArray;
            DotDensityRenderer.DotDensitySymbol = ddSym;
            //ȷ��һ����������ֵ
            DotDensityRenderer.DotValue = pValue;
            //���ܶ���Ⱦ���õ���ɫģʽ
            DotDensityRenderer.ColorScheme = "Custom";
            //�������ܶ�ͼ��Ⱦͼ��
            DotDensityRenderer.CreateLegend();
            //���÷��Ŵ�С�Ƿ�̶�
            DotDensityRenderer.MaintainSize = true;
            //�����ܶ�ͼ��Ⱦ��������Ⱦͼ��ҹ�
            pGeoFeatureLayer.Renderer = (IFeatureRenderer)DotDensityRenderer;
            //ˢ�µ�ͼ��TOOCotrol
            IActiveView pActiveView = pMap as IActiveView;
            pActiveView.Refresh();
                 







        }
        public IRgbColor GetRGBColor(int r, int g, int b)
        {
            IRgbColor pRGB;

            pRGB = new RgbColorClass();

            pRGB.Red = r;

            pRGB.Green = g;

            pRGB.Blue = b;

            return pRGB;


        }
    }
}
