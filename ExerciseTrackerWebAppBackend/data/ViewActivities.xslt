<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet 
		version="2.0" 
		xmlns:xsl="http://www.w3.org/1999/XSL/Transform" 
		exclude-result-prefixes="xsl user dept" 
		xmlns:dept="http://tempuri.org/ExerciseTracker"
		xmlns="http://www.w3.org/1999/xhtml" 
		xmlns:msxsl="urn:schemas-microsoft-com:xslt" 
		xmlns:user="urn:my-scripts"
		xmlns:fn="http://www.w3.org/2005/02/xpath-functions"
        xmlns:xs="http://www.w3.org/2001/XMLSchema"
	>
	<xsl:output method="html" indent="yes"/>

	<xsl:template match="/">
		<xsl:variable name="time"/>
		<table class="people-table">
			<thead>
				<tr>
					<th>ID</th>
					<th>Type</th>
					<th>Date</th>
					<th>Duration</th>
					<th>Distance</th>
				</tr>
			</thead>
			<tbody>
				<xsl:for-each select="//dept:Activity" xml:space="preserve">
					<xsl:variable name="activityId" select="dept:TypeId"></xsl:variable>
					<tr>
						<td><xsl:value-of select="@Id"/></td>
						<td>
							<xsl:value-of select="document('Types.xml')/dept:Types/dept:Type[@Id=$activityId]/dept:Name"/>
							<!--<xsl:value-of select="dept:TypeId"/>-->
						</td>
						<td><xsl:value-of select="dept:Date"/></td>
						<td><xsl:value-of select="dept:Duration"/></td>
						<td><xsl:value-of select="dept:Distance"/></td>
					</tr>

				</xsl:for-each>
				<tr>
					<td></td>
					<td></td>
					<td></td>
					<td>Tot Time: <xsl:value-of select="sum(//dept:Duration)"/> </td>
					<td>Tot Dist: <xsl:value-of select="sum(//dept:Distance)"/></td>
				</tr>
				<tr>
					<td></td>
					<td></td>
					<td></td>
					<td>Avg Time: <xsl:value-of select="format-number(sum(//dept:Duration) div count(//dept:Distance), '#.00')"/></td>
					<td>Avg Dist: <xsl:value-of select="format-number(sum(//dept:Distance) div count(//dept:Distance), '#.00')"/></td>
				</tr>
			</tbody>
		</table>
	</xsl:template>
</xsl:stylesheet>
