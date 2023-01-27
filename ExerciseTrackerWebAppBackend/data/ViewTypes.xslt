<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet 
	version="1.0" 
	xmlns:dept="http://tempuri.org/ExerciseTracker"
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" 
	exclude-result-prefixes="msxsl dept"
>
    <xsl:output method="html" indent="yes"/>

    <xsl:template match="/">
		<ul class="activityType-list">
			<xsl:for-each select="//dept:Type" xml:space="preserve">
				<li>
					<span class="activityType-id"><xsl:value-of select="@Id"/></span>
					<span class="activityType-name"><xsl:value-of select="dept:Name"/></span>
				</li>
			</xsl:for-each>
		</ul>
    </xsl:template>
</xsl:stylesheet>
